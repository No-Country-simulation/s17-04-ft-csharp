using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Technology;
using JunioHub.Application.Exceptions;
using JunioHub.Application.Validators.Technology;
using JuniorHub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JunioHub.Application.Services;

public class TechnologyService : ITechnologyService
{
    private readonly ITechnologyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<TechnologyService> _logger;

    public TechnologyService(
        ITechnologyRepository repository,
        IMapper mapper,
        ILogger<TechnologyService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BaseResponse<TechnologiesDto>> AddTechnology(TechnologyAddDto technologyToAdd)
    {
        var baseResponse = new BaseResponse<TechnologiesDto>();

        var validator = new AddTechnologyValidator();
        var validationResult = await validator.ValidateAsync(technologyToAdd);
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
                var newTechnology = _mapper.Map<Technology>(technologyToAdd);

                var techonologyCreated = await _repository.AddAsync(newTechnology);
                await _repository.SaveChangesAsync();

                baseResponse.Data = _mapper.Map<TechnologiesDto>(techonologyCreated);
                baseResponse.Message = "New technology added successfully.";
            }
            catch (Exception ex)
            {
                baseResponse.Success = false;
                baseResponse.Message = ex.Message;
                _logger.LogError(ex, "An error occurred while adding technology.");
            }
        }

        return baseResponse;
    }

    public async Task<BaseResponse<TechnologyGetByIdDto>> GetTechnologyById(int technologyId)
    {
        var baseResponse = new BaseResponse<TechnologyGetByIdDto>();
        try
        {
            baseResponse.Data = await _repository.GetByIdAsyncProjectTo<TechnologyGetByIdDto>(technologyId);
            if (baseResponse.Data == null)
            {
                throw new NotFoundException(nameof(Technology), technologyId);
            }
        }
        catch (Exception ex)
        {
            baseResponse.Success = false;
            baseResponse.Message = ex.Message;
            _logger.LogError(ex, $"{ex.Message}");
        }

        return baseResponse;
    }

    public async Task<BaseResponse<List<TechnologiesDto>>> GetAllTechnologies()
    {
        var baseResponse = new BaseResponse<List<TechnologiesDto>>();

        try
        {
            baseResponse.Data = await _repository.GetAllAsyncProjectTo<TechnologiesDto>();
        }
        catch (Exception ex)
        {
            baseResponse.Success = false;
            baseResponse.Message = ex.Message;
            _logger.LogError(ex.Message);
        }

        return baseResponse;
    }

    public async Task<BaseResponse<bool>> UpdateTechnology(int technologyId, TechnologyUpdateDto technologyToUpdate)
    {
        var baseResponse = new BaseResponse<bool>();

        var technology = await _repository.GetByIdAsync(technologyId);
        if (technology == null)
        {
            throw new NotFoundException(nameof(Technology), technologyId);
        }

        var technologyToUpdateEntity = _mapper.Map(technologyToUpdate, technology);
        _repository.Update(technologyToUpdateEntity);

        await _repository.SaveChangesAsync();

        baseResponse.Data = true;
        baseResponse.Message = "Technology edited successfully.";

        return baseResponse;
    }

    public async Task<BaseResponse<bool>> DeleteTechnology(int technologyId)
    {
        var baseResponse = new BaseResponse<bool>();

        var technology = await _repository.GetByIdAsync(technologyId);

        if (technology == null)
        {
            throw new NotFoundException(nameof(Technology), technologyId);
        }

        if (await _repository.DeleteAsync(technologyId))
        {
            await _repository.SaveChangesAsync();

            baseResponse.Data = true;
            baseResponse.Message = "Technology deleted successfully";
        }

        return baseResponse;
    }

}