using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Persistence;

public interface IFreelancerRepository : IGenericRepository<Freelancer>
{
    Task<Freelancer?> GetProfileFreelancer(int userId);
    Task<Freelancer?> GetFreelancerForValoration(int userId);
    Task<bool> FreelancerIdExistsAsync(int id);
}
