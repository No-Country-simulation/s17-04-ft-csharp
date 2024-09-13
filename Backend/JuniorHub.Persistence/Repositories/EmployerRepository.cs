using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using AutoMapper;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

public class EmployerRepository : GenericRepository<Employer>, IEmployerRepository
{
    public EmployerRepository(JuniorHubContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }

    public async Task<Employer?> GetProfileEmployer(int userId)
    {
        var employer = await _dbContext.Employers
                                .Include(e => e.Offers) 
                                .ThenInclude(o => o.Technologies) 
                                .FirstOrDefaultAsync(e => e.UserId == userId);
        return employer;
    }

    public async Task<int> GetEmployerId(int userId)
    {
        return await _dbContext.Employers
            .Where(f => f.UserId == userId)
            .Select(f => f.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> EmployerIdExistsAsync(int id)
    {
        return await _dbContext.Employers
            .AnyAsync(f => f.Id == id);
    }
}