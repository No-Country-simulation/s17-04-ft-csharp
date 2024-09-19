using JuniorHub.Application.DTOs.User;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Application.Contracts.Persistence;

public interface IFreelancerRepository : IGenericRepository<Freelancer>
{
    Task<Freelancer?> GetProfileFreelancer(int userId);
    Task<int> GetFreelancerId(int userId);
    Task<bool> FreelancerIdExistsAsync(int id);
    Task<UserSendGridDto?> GetFreelancerUser(int freelancerId);
}
