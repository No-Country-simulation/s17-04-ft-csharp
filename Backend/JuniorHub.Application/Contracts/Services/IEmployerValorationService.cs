using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Application.DTOs;

namespace JuniorHub.Application.Contracts.Services;

public interface IEmployerValorationService
{
    Task<BaseResponse<ValorationDto>> AddEmployerValoration(int userId, ValorationToEmployerDto valorationEmployer);
}
