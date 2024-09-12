using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

public class FreelancerRepository : GenericRepository<Freelancer>, IFreelancerRepository
{
    public FreelancerRepository(JuniorHubContext dbContext, IMapper mapper) : base(dbContext, mapper)
    {
    }

    public async Task<Freelancer?> GetProfileFreelancer(int userId)
    {
        var user = await _dbContext.Freelancers
            .Include(f => f.Technologies)
            .Include(f => f.Links)
            .FirstOrDefaultAsync(f => f.UserId == userId);

        return user;
    }

    public async Task<int> GetFreelancerId(int userId)
    {
        return await _dbContext.Freelancers
            .Where(f => f.UserId == userId)
            .Select(f => f.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> FreelancerIdExistsAsync(int id)
    {
        return await _dbContext.Freelancers
            .AnyAsync(f => f.Id == id);
    }
}
