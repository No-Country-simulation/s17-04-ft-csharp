
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Employer;

namespace JunioHub.Application.Contracts.Services
{
    public interface IEmployerService
    {
        Task<BaseResponse<EmployersDto>>AddEmployer(EmployerAddDto employer,int idUser);
       // Task<BaseResponse<List<EmployerProfileDto>>> GetAllEmployers(int idUser);
        Task<BaseResponse<EmployerProfileDto>> GetProfileEmployer(int idUser);
        Task<BaseResponse<EmployerUpdateDto>> UpdateEmployer(EmployerUpdateDto employerUpdateDto,int idUser);
}
}