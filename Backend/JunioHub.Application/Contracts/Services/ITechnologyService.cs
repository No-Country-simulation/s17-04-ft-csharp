using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Technology;

namespace JunioHub.Application.Contracts.Services;

public interface ITechnologyService
{
    Task<BaseResponse<TechnologiesDto>> AddTechnology(TechnologyAddDto technology);
    Task<BaseResponse<TechnologyGetByIdDto>> GetTechnologyById(int technologyId);
    Task<BaseResponse<List<TechnologiesDto>>> GetAllTechnologies();
    Task<BaseResponse<bool>> UpdateTechnology(int technologyId, TechnologyUpdateDto technologyToUpdate);
    Task<BaseResponse<bool>> DeleteTechnology(int technologyId);
}
