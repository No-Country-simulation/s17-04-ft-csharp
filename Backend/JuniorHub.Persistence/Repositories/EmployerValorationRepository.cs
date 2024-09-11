using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
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

}