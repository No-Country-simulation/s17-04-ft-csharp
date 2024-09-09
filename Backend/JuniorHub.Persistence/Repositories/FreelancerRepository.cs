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

    public async Task<Freelancer?> GetFreelancerForValoration(int userId)
    {
        var freelancer = await _dbContext.Freelancers
            .Include(f => f.User)
            .FirstOrDefaultAsync(f => f.UserId == userId);

        return freelancer;
    }
}
