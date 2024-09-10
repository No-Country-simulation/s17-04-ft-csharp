using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

public class ValorationRepository : GenericRepository<Valoration>, IValorationRepository
{
    public ValorationRepository(JuniorHubContext dbContext, IMapper mapper) 
        : base(dbContext, mapper)
    {
    }

    public async Task<bool> ValorationExistsAsync(
        string reviewer, 
        int freelancerId, 
        int employerId)
    {
        return await _dbContext.Valorations
            .AnyAsync(v => v.Reviewer == reviewer && 
                           v.FreelancerId == freelancerId && 
                           v.EmployerId == employerId);
    }

}