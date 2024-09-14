using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Application;
using JuniorHub.Application.Exceptions;
using JuniorHub.Application.Validators.Application;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace JuniorHub.Application.Services;

public class OfferApplicationService : IOfferApplicationService
{
    private readonly IOfferApplicationRepository _applicationRepository;
    private readonly IOfferRepository _offerRepository;
    private readonly IFreelancerRepository _freelancerRepository;
    private readonly IEmployerRepository _employerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<OfferApplicationService> _logger;


    public OfferApplicationService(
        IOfferApplicationRepository applicationRepository,
        IOfferRepository offerRepository,
        IFreelancerRepository freelancerRepository,
        IEmployerRepository employerRepository,
        IMapper mapper,
        ILogger<OfferApplicationService> logger)
    {
        _offerRepository = offerRepository;
        _applicationRepository = applicationRepository;
        _freelancerRepository = freelancerRepository;
        _employerRepository = employerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponse<bool>> ApplyToOfferAsync(int userId, ApplyOfferDto applyOfferDto)
    {
        var baseResponse = new BaseResponse<bool>();

        var validator = new ApplyOfferValidator();
        var validationResult = await validator.ValidateAsync(applyOfferDto);
        if (validationResult.Errors.Count > 0)
        {
            baseResponse.Success = false;
            baseResponse.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                baseResponse.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        if (baseResponse.Success)
        {

            var freelancerId = await _freelancerRepository.GetFreelancerId(userId);
            if (freelancerId == null)
            {
                throw new NotFoundException(nameof(Freelancer), freelancerId);
            }

            var offer = await _offerRepository.GetByIdAsync(applyOfferDto.OfferId);
            if (offer == null || offer.State != State.Open)
            {
                throw new NotFoundException(nameof(Offer), applyOfferDto.OfferId);
            }

            var existingApplication = await _applicationRepository
                .ApplicationOfferExistsAsync(freelancerId, applyOfferDto.OfferId);

            if (existingApplication)
            {
                throw new BadRequestException("You have already applied to this offer");
            }

            try
            {
                var application = _mapper.Map<JuniorHub.Domain.Entities.OfferApplication>(applyOfferDto);
                application.FreelancerId = freelancerId;

                await _applicationRepository.AddAsync(application);
                await _applicationRepository.SaveChangesAsync();

                baseResponse.Data = true;
                baseResponse.Message = "Apply to offer successfully.";
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = ex.Message;
                _logger.LogError(ex, "An error occurred while apply to offer.");
            }
        }

        return baseResponse;
    }

    public async Task<BaseResponse<bool>> DeleteApplicationAsync(int userId, int applicationId)
    {
        var freelancerId = await _freelancerRepository.GetFreelancerId(userId);
        if (freelancerId == 0)
        {
            throw new NotFoundException(nameof(Freelancer), freelancerId);
        }

        var existingApplication = await _applicationRepository
                .FreelancerApplicationExistsAsync(freelancerId, applicationId);

        if (!existingApplication)
        {
            throw new NotFoundException(nameof(JuniorHub.Domain.Entities.OfferApplication), applicationId);
        }

        var baseResponse = new BaseResponse<bool>();

        try
        {
            await _applicationRepository.DeleteAsync(applicationId);
            await _applicationRepository.SaveChangesAsync();

            baseResponse.Data = true;
            baseResponse.Message = "Apply delete successfully.";
        }
        catch (Exception ex)
        {
            baseResponse.Success = false;
            baseResponse.Message = ex.Message;
            _logger.LogError(ex, "An error occurred while delete application.");
        }
        return baseResponse;
    }

    public async Task<BaseResponse<IEnumerable<ApplicationByOfferDto?>>> GetApplicationsByOfferIdAsync(int userId, int offerId)
    {
        var employerId = await _employerRepository.GetEmployerId(userId);
        if (employerId == 0)
        {
            throw new NotFoundException(nameof(Employer), employerId);
        }

        var existingOffer = await _offerRepository
                .EmployerOfferExistsAsync(employerId, offerId);

        if (existingOffer)
        {
            throw new NotFoundException(nameof(Offer), offerId);
        }

        var baseResponse = new BaseResponse<IEnumerable<ApplicationByOfferDto>>();

        var applications = await _applicationRepository
            .GetByPropertyAsyncProjectTo<ApplicationByOfferDto>("OfferId", offerId);

        baseResponse.Data = applications;
        baseResponse.Message = "List of applications by offers.";

        return baseResponse;
    }

    public async Task<BaseResponse<bool>> SelectApplicationAsync(int userId, int offerId, int applicationId)
    {
        var employerId = await _employerRepository.GetEmployerId(userId);
        if (employerId == 0)
        {
            throw new NotFoundException(nameof(Employer), employerId);
        }

        var existingOffer = await _offerRepository.EmployerOfferExistsAsync(employerId, offerId);
        if (!existingOffer)
        {
            throw new NotFoundException(nameof(Offer), offerId, employerId);
        }

        var application = await _applicationRepository.GetByIdAsync(applicationId);
        if (application == null || application.OfferId != offerId)
        {
            throw new NotFoundException(nameof(OfferApplication), applicationId);
        }

        var baseResponse = new BaseResponse<bool>();
        try
        {
            application.Selected = true;
            _applicationRepository.Update(application);
            await _applicationRepository.SaveChangesAsync();

            var offer = await _offerRepository.GetByIdAsync(offerId);
            offer.State = State.Closed;
            _offerRepository.Update(offer);
            await _offerRepository.SaveChangesAsync();

            baseResponse.Data = true;
            baseResponse.Message = "Application selected and offer closed.";
        }
        catch (Exception ex)
        {
            baseResponse.Success = false;
            baseResponse.Message = ex.Message;
            _logger.LogError(ex, "An error occurred while update Offer Application.");
        }
        return baseResponse;
    }

}
