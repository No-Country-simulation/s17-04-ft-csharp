using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Freelancer;

namespace JuniorHub.Application.Contracts.Services
{
    public interface IFreelancerService
    {
        Task<BaseResponse<FreelancerProfileDto>> GetProfileFreelancer(int idUser);
        Task<BaseResponse<FreelancerDto>> AddFreelancer(int idUser);
        Task<BaseResponse<FreelancerProfileDto>> UpdateFreelancer(FreelancerUpdateDto freelancerUpdateDto, int idUser);
    }
}
