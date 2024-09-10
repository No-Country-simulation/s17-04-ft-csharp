using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs.Valoration;
using JunioHub.Application.DTOs;
using JunioHub.Application.Exceptions;
using JunioHub.Application.Validators.Valoration;
using JuniorHub.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace JunioHub.Application.Services;

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
                var newValoration = _mapper.Map<FreelancerValoration>(valorationFreelancerDto);
                newValoration.EmployerId = employer.Id;

                var existsValoration = await _freelancerValorationRepository
                    .ValorationExistsAsync(newValoration.FreelancerId, newValoration.EmployerId);

                if (existsValoration)
                {
                    throw new BadRequestException("A valoration with the same Reviewer, FreelancerId, and EmployerId already exists.");
                }

                var valorationCreated = await _freelancerValorationRepository.AddAsync(newValoration);
                await _freelancerValorationRepository.SaveChangesAsync();

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

    public async Task<BaseResponse<IEnumerable<FreelancerValorationDto>>> GetAllValorationsForFreelancerAsync(int freelancerId)
    {
        var baseResponse = new BaseResponse<IEnumerable<FreelancerValorationDto>>();

        var freelancerExists = await _freelancerValorationRepository
            .FreelancerIdExistsAsync(freelancerId);

        if (!freelancerExists)
        {
            throw new NotFoundException(nameof(FreelancerValoration), freelancerId);
        }


        try
        {
            var freelancerValorations = await _freelancerValorationRepository.GetAllByFreelancerIdAsync(freelancerId);
            var freelancerValorationsResponse = _mapper.Map<IEnumerable<FreelancerValorationDto>>(freelancerValorations);
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

}