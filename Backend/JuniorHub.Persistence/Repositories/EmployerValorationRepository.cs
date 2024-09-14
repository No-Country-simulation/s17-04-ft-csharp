using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

public class EmployerValorationRepository : GenericRepository<EmployerValoration>, IEmployerValorationRepository
{
    public EmployerValorationRepository(JuniorHubContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }

    public async Task<bool> ValorationExistsAsync(int freelancerId, int employerId)
    {
        return await _dbContext.EmployerValorations
            .AnyAsync(v => v.FreelancerId == freelancerId &&
                           v.EmployerId == employerId);
    }

    public async Task<bool> EmployerIdExistsAsync(int employerId)
    {
        return await _dbContext.EmployerValorations
            .AnyAsync(v => v.EmployerId == employerId);
    }

    public async Task<IEnumerable<EmployerValoration>> GetReviewersByEmployerIdAsync(int employerId)
    {
        return await _dbContext.EmployerValorations
                             .Where(v => v.EmployerId == employerId)
                             .Include(v => v.Freelancer)
                             .ThenInclude(e => e.User)
                             .ToListAsync();
    }

    public async Task<IEnumerable<ValorationEnum>> GetValorationValuesByEmployerIdAsync(int employerId)
    {
        return await _dbContext.EmployerValorations
                                .Where(v => v.EmployerId == employerId)
                                .Select(v => v.ValorationValue)
                                .ToListAsync();
    }
}