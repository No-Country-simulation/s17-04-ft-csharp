using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.Contracts.Persistence;

public interface IFreelancerValorationRepository : IGenericRepository<FreelancerValoration>
{
    Task<bool> ValorationExistsAsync(int freelancerId, int employerId);
    Task<bool> FreelancerIdExistsAsync(int freelancerId);
    Task<IEnumerable<FreelancerValoration>> GetReviewersByFreelancerIdAsync(int freelancerId);
    Task<IEnumerable<ValorationEnum>> GetValorationValuesByFreelancerIdAsync(int freelancerId);
}
