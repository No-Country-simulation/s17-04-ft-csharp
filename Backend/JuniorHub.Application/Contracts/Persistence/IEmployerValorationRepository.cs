using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.Contracts.Persistence;

public interface IEmployerValorationRepository : IGenericRepository<EmployerValoration>
{
    Task<bool> ValorationExistsAsync(int freelancerId, int employerId);
    Task<bool> EmployerIdExistsAsync(int employerId);
    Task<IEnumerable<EmployerValoration>> GetReviewersByEmployerIdAsync(int employerId);
    Task<IEnumerable<ValorationEnum>> GetValorationValuesByEmployerIdAsync(int employerId);
}
