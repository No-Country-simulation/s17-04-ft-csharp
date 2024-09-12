using JuniorHub.Domain.Entities;

namespace JuniorHub.Application.Contracts.Persistence;

public interface IEmployerValorationRepository : IGenericRepository<EmployerValoration>
{
    Task<bool> ValorationExistsAsync(int freelancerId, int employerId);
}
