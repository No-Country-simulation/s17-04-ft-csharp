
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Employer;

namespace JunioHub.Application.Contracts.Services
{
    public interface IEmployerService
    {
        Task<BaseResponse<EmployerProfileDto>> GetProfileEmployer(int idUser);
        Task<BaseResponse<EmployerUpdateDto>> UpdateEmployer(EmployerUpdateDto employerUpdateDto,int idUser);
}
}