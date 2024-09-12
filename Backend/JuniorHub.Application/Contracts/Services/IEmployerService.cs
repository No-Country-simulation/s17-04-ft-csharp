
using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Employer;

namespace JuniorHub.Application.Contracts.Services;

public interface IEmployerService
{
    Task<BaseResponse<EmployerProfileDto>> GetProfileEmployer(int idUser);
    Task<BaseResponse<EmployerUpdateDto>> UpdateEmployer(EmployerUpdateDto employerUpdateDto, int idUser);
}