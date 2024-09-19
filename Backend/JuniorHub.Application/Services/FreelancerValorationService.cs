using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Application.DTOs;
using JuniorHub.Application.Exceptions;
using JuniorHub.Application.Validators.Valoration;
using JuniorHub.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.Services;

public class FreelancerValorationService : IFreelancerValorationService
{
    private readonly IFreelancerValorationRepository _freelancerValorationRepository;
    private readonly IFreelancerRepository _freelancerRepository;
    private readonly IEmployerRepository _employerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<FreelancerValorationService> _logger;

    public FreelancerValorationService(
        IFreelancerValorationRepository freelancerValorationRepository,
        IFreelancerRepository freelancerRepository,
        IEmployerRepository employerRepository,
        IMapper mapper,
        ILogger<FreelancerValorationService> logger)
    {
        _freelancerValorationRepository = freelancerValorationRepository;
        _freelancerRepository = freelancerRepository;
        _employerRepository = employerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponse<ValorationAddDto>> AddFreelancerValoration(int userId, ValorationToFreelancerDto valorationFreelancerDto)
    {
        var baseResponse = new BaseResponse<ValorationAddDto>();

        var employerId = await _employerRepository.GetEmployerId(userId);
        if (employerId == 0)
        {
            throw new NotFoundException(nameof(Employer), employerId);
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
                var newValoration = _mapper.Map<FreelancerValoration>(valorationFreelancerDto);
                newValoration.EmployerId = employerId;

                var existsValoration = await _freelancerValorationRepository
                    .ValorationExistsAsync(newValoration.FreelancerId, newValoration.EmployerId);

                if (existsValoration)
                {
                    throw new BadRequestException("A valoration with the same Reviewer FreelancerId and EmployerId already exists.");
                }

                var valorationCreated = await _freelancerValorationRepository.AddAsync(newValoration);
                await _freelancerValorationRepository.SaveChangesAsync();

                await UpdateFreelancerAverageValorationAsync(newValoration.FreelancerId);

                baseResponse.Data = _mapper.Map<ValorationAddDto>(valorationCreated);
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

    public async Task<BaseResponse<IEnumerable<ValorationResponseDto>>> GetAllValorationsForFreelancerAsync(int freelancerId)
    {
        var baseResponse = new BaseResponse<IEnumerable<ValorationResponseDto>>();

        var freelancerExists = await _freelancerValorationRepository
            .FreelancerIdExistsAsync(freelancerId);

        if (!freelancerExists)
        {
            throw new NotFoundException(nameof(FreelancerValoration), freelancerId);
        }

        try
        {
            var freelancerValorations = await _freelancerValorationRepository
                                                .GetReviewersByFreelancerIdAsync(freelancerId);

            var freelancerValorationsResponse = _mapper.Map<IEnumerable<ValorationResponseDto>>(freelancerValorations);
            baseResponse.Data = freelancerValorationsResponse;
        }
        catch (Exception ex)
        {
            baseResponse.Success = false;
            baseResponse.Message = ex.Message;
            _logger.LogError(ex.Message);
        }

        return baseResponse;
    }

    private async Task UpdateFreelancerAverageValorationAsync(int freelancerId)
    {
        var valorationValues = await _freelancerValorationRepository
            .GetValorationValuesByFreelancerIdAsync(freelancerId);

        var averageValoration = valorationValues.Any()
            ? valorationValues.Average(v => (int)v)
            : 0;

        var roundedAverage = (ValorationEnum)Math.Round(averageValoration);

        var freelancer = await _freelancerRepository.GetByIdAsync(freelancerId);
        if (freelancer != null)
        {
            freelancer.Valoration = roundedAverage;
            _freelancerRepository.Update(freelancer);
            await _freelancerRepository.SaveChangesAsync();
        }
    }

}