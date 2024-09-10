using JunioHub.Application.DTOs.Valoration;
using JunioHub.Application.DTOs;

namespace JunioHub.Application.Contracts.Services;

public interface IEmployerValorationService
{
    Task<BaseResponse<ValorationDto>> AddEmployerValoration(int userId, ValorationToEmployerDto valorationEmployer);
}
