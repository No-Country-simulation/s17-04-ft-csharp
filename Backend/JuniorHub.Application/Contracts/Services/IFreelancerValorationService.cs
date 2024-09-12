using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Application.DTOs;

namespace JuniorHub.Application.Contracts.Services;

public interface IFreelancerValorationService
{
    Task<BaseResponse<ValorationDto>> AddFreelancerValoration(int userId, ValorationToFreelancerDto valorationFreelancer);
    Task<BaseResponse<IEnumerable<FreelancerValorationDto>>> GetAllValorationsForFreelancerAsync(int freelancerId);
}
