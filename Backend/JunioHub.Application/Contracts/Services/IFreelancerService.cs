using JunioHub.Application.DTOs.Technology;
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Freelancer;
using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Services
{
    public interface IFreelancerService
    {
        Task<BaseResponse<FreelancerProfileDto>> GetProfileFreelancer(int idUser);
        Task<BaseResponse<FreelancerDto>> AddFreelancer(FreelancerAddDto freelancer, int idUser);
    }
}
