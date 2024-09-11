using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs.Valoration;
using JunioHub.Application.DTOs;
using JunioHub.Application.Exceptions;
using JunioHub.Application.Validators.Valoration;
using JuniorHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JunioHub.Application.Services;

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
                var newValoration = _mapper.Map<EmployerValoration>(valorationEmployerDto);
                newValoration.FreelancerId = freelancer.Id;
                //newValoration.Reviewer = $"{freelancer.User.LastName}, {freelancer.User.Name}";
                //newValoration.Reviewer = freelancer.User.Email;

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