using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Application.DTOs;

namespace JuniorHub.Application.Contracts.Services;

public interface IEmployerValorationService
{
    Task<BaseResponse<ValorationAddDto>> AddEmployerValoration(int userId, ValorationToEmployerDto valorationEmployer);
    Task<BaseResponse<IEnumerable<ValorationResponseDto>>> GetAllValorationsForEmployerAsync(int employerId);
}
