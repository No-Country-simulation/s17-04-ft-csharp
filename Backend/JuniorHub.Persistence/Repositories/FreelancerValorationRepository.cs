using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

public class FreelancerValorationRepository : GenericRepository<FreelancerValoration>, IFreelancerValorationRepository
{
    public FreelancerValorationRepository(JuniorHubContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }

    public async Task<bool> ValorationExistsAsync(int freelancerId, int employerId)
    {
        return await _dbContext.FreelancerValorations
            .AnyAsync(v => v.FreelancerId == freelancerId &&
                           v.EmployerId == employerId);
    }

    public async Task<bool> FreelancerIdExistsAsync(int freelancerId)
    {
        return await _dbContext.FreelancerValorations
            .AnyAsync(v => v.FreelancerId == freelancerId);
    }

    public async Task<IEnumerable<FreelancerValoration>> GetAllByFreelancerIdAsync(int freelancerId)
    {
        return await _dbContext.FreelancerValorations
                             .Where(v => v.FreelancerId == freelancerId)
                             .Include(v => v.Employer)
                             .ThenInclude(e => e.User)
                             .ToListAsync();
    }
}