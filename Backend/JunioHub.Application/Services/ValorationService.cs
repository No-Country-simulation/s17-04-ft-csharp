using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Valoration;
using JunioHub.Application.Exceptions;
using JunioHub.Application.Validators.Valoration;
using JuniorHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JunioHub.Application.Services;

public class ValorationService : IValorationService
{
    private readonly IValorationRepository _valorationRepository;
    private readonly IFreelancerRepository _freelancerRepository;
    private readonly IEmployerRepository _employerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<ValorationService> _logger;

    public ValorationService(
        IValorationRepository valorationRepository,
        IFreelancerRepository freelancerRepository,
        IEmployerRepository employerRepository,
        IMapper mapper,
        ILogger<ValorationService> logger)
    {
        _valorationRepository = valorationRepository;
        _freelancerRepository = freelancerRepository;
        _employerRepository = employerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponse<ValorationDto>> AddFreelancerValoration(int userId, ValorationToFreelancerDto valorationFreelancerDto)
    {
        var baseResponse = new BaseResponse<ValorationDto>();

        var employer = await _employerRepository.GetEmployerForValoration(userId);
        if (employer == null)
        {
            throw new NotFoundException(nameof(Employer), employer.Id);
        }

        var freelancerIdExists = await _freelancerRepository
            .FreelancerIdExistsAsync(valorationFreelancerDto.FreelancerId);

        if (!freelancerIdExists)
        {
            throw new NotFoundException(nameof(Freelancer), valorationFreelancerDto.FreelancerId);
        }

        var validator = new AddValorationToFreelancerValidator();
        var validationResult = await validator.ValidateAsync(valorationFreelancerDto);
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
            try
            {
                var newValoration = _mapper.Map<Valoration>(valorationFreelancerDto);
                newValoration.EmployerId = employer.Id;
                //newValoration.Reviewer = $"{freelancer.User.LastName}, {freelancer.User.Name}";
                newValoration.Reviewer = employer.User.Email;

                var existsValoration = await _valorationRepository
                    .ValorationExistsAsync(newValoration.Reviewer, newValoration.FreelancerId, newValoration.EmployerId);

                if (existsValoration)
                {
                    throw new BadRequestException("A valoration with the same Reviewer, FreelancerId, and EmployerId already exists.");
                }

                var valorationCreated = await _valorationRepository.AddAsync(newValoration);
                await _valorationRepository.SaveChangesAsync();

                baseResponse.Data = _mapper.Map<ValorationDto>(valorationCreated);
                baseResponse.Message = "New valoration added successfully.";
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = ex.Message;
                _logger.LogError(ex, "An error occurred while adding valoration.");
            }
        }

        return baseResponse;
    }


    public async Task<BaseResponse<ValorationDto>> AddEmployerValoration(int userId, ValorationToEmployerDto valorationEmployerDto)
    {
        var baseResponse = new BaseResponse<ValorationDto>();

        var freelancer = await _freelancerRepository.GetFreelancerForValoration(userId);
        if (freelancer == null)
        {
            throw new NotFoundException(nameof(Freelancer), freelancer.Id);
        }

        var employerIdExists = await _employerRepository
            .EmployerIdExistsAsync(valorationEmployerDto.EmployerId);

        if (!employerIdExists)
        {
            throw new NotFoundException(nameof(Employer), valorationEmployerDto.EmployerId);
        }

        var validator = new AddValorationToEmployerValidator();
        var validationResult = await validator.ValidateAsync(valorationEmployerDto);
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
            try
            {
                var newValoration = _mapper.Map<Valoration>(valorationEmployerDto);
                newValoration.FreelancerId = freelancer.Id;
                //newValoration.Reviewer = $"{freelancer.User.LastName}, {freelancer.User.Name}";
                newValoration.Reviewer = freelancer.User.Email;

                var existsValoration = await _valorationRepository
                    .ValorationExistsAsync(newValoration.Reviewer, newValoration.FreelancerId, newValoration.EmployerId);

                if (existsValoration)
                {
                    throw new BadRequestException("A valoration with the same Reviewer, FreelancerId, and EmployerId already exists.");
                }

                var valorationCreated = await _valorationRepository.AddAsync(newValoration);
                await _valorationRepository.SaveChangesAsync();

                baseResponse.Data = _mapper.Map<ValorationDto>(valorationCreated);
                baseResponse.Message = "New valoration added successfully.";
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = ex.Message;
                _logger.LogError(ex, "An error occurred while adding valoration.");
            }
        }

        return baseResponse;
    }

}