using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Application.DTOs;
using JuniorHub.Application.Exceptions;
using JuniorHub.Application.Validators.Valoration;
using JuniorHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JuniorHub.Application.Services;

public class EmployerValorationService : IEmployerValorationService
{
    private readonly IEmployerValorationRepository _employerValorationRepository;
    private readonly IEmployerRepository _employerRepository;
    private readonly IFreelancerRepository _freelancerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<EmployerValorationService> _logger;

    public EmployerValorationService(
        IEmployerValorationRepository employerValorationRepository,
        IEmployerRepository employerRepository,
        IFreelancerRepository freelancerRepository,
        IMapper mapper,
        ILogger<EmployerValorationService> logger)
    {
        _employerValorationRepository = employerValorationRepository;
        _employerRepository = employerRepository;
        _freelancerRepository = freelancerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponse<ValorationDto>> AddEmployerValoration(int userId, ValorationToEmployerDto valorationEmployerDto)
    {
        var baseResponse = new BaseResponse<ValorationDto>();

        var freelancerId = await _freelancerRepository.GetFreelancerId(userId);
        if (freelancerId == 0)
        {
            throw new NotFoundException(nameof(Freelancer), freelancerId);
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
                var newValoration = _mapper.Map<EmployerValoration>(valorationEmployerDto);
                newValoration.FreelancerId = freelancerId;

                var existsValoration = await _employerValorationRepository
                    .ValorationExistsAsync(newValoration.FreelancerId, newValoration.EmployerId);

                if (existsValoration)
                {
                    throw new BadRequestException("A valoration with the same Reviewer, FreelancerId, and EmployerId already exists.");
                }

                var valorationCreated = await _employerValorationRepository.AddAsync(newValoration);
                await _employerValorationRepository.SaveChangesAsync();

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