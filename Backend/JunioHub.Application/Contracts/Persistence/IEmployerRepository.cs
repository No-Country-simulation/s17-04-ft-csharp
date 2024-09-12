using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Persistence;

public interface IEmployerRepository :IGenericRepository<Employer>
{
    Task<Employer?> GetProfileEmployer(int userId);
    Task<bool> EmployerIdExistsAsync(int id);
    Task<int> GetEmployerId(int userId);
}