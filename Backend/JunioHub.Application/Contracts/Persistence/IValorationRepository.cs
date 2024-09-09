using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Persistence;

public interface IValorationRepository : IGenericRepository<Valoration>
{
    Task<bool> ValorationExistsAsync(string reviewer, int freelancerId, int employerId);
}