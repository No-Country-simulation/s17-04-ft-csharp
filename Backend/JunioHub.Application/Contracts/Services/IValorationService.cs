using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Valoration;

namespace JunioHub.Application.Contracts.Services;

public interface IValorationService
{
    Task<BaseResponse<ValorationDto>> AddEmployerValoration(int userId, ValorationToEmployerDto valorationEmployer);
    Task<BaseResponse<ValorationDto>> AddFreelancerValoration(int userId, ValorationToFreelancerDto valorationFreelancer);
}
