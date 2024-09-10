using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Persistence;

public interface IEmployerValorationRepository : IGenericRepository<EmployerValoration>
{
    Task<bool> ValorationExistsAsync(int freelancerId, int employerId);
}
