using JunioHub.Application.DTOs.Valoration;
using JunioHub.Application.DTOs;

namespace JunioHub.Application.Contracts.Services;

public interface IFreelancerValorationService
{
    Task<BaseResponse<ValorationDto>> AddFreelancerValoration(int userId, ValorationToFreelancerDto valorationFreelancer);
    Task<BaseResponse<IEnumerable<FreelancerValorationDto>>> GetAllValorationsForFreelancerAsync(int freelancerId);
}
