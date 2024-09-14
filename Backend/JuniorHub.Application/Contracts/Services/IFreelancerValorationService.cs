using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Application.DTOs;

namespace JuniorHub.Application.Contracts.Services;

public interface IFreelancerValorationService
{
    Task<BaseResponse<ValorationAddDto>> AddFreelancerValoration(int userId, ValorationToFreelancerDto valorationFreelancer);
    Task<BaseResponse<IEnumerable<ValorationResponseDto>>> GetAllValorationsForFreelancerAsync(int freelancerId);
}
