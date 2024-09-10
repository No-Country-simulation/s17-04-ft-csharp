using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Persistence;

public interface IFreelancerValorationRepository : IGenericRepository<FreelancerValoration>
{
    Task<bool> ValorationExistsAsync(int freelancerId, int employerId);
    Task<bool> FreelancerIdExistsAsync(int freelancerId);
    Task<IEnumerable<FreelancerValoration>> GetAllByFreelancerIdAsync(int freelancerId);
}
