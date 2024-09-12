using AutoMapper;
using AutoMapper.QueryableExtensions;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.DTOs.Application;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

internal class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
{
    public ApplicationRepository(JuniorHubContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }

    public async Task<bool> ApplicationOfferExistsAsync(int freelancerId, int offerId)
    {
        return await _dbContext.Applications
                        .AnyAsync(v => v.FreelancerId == freelancerId &&
                                       v.OfferId == offerId);
    }

    public async Task<bool> FreelancerApplicationExistsAsync(int freelancerId, int applicationId)
    {
        return await _dbContext.Applications
                        .AnyAsync(v => v.Id == applicationId &&
                                       v.FreelancerId == freelancerId); 
    }
}
