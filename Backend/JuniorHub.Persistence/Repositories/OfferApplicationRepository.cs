using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

internal class OfferApplicationRepository : GenericRepository<OfferApplication>, IOfferApplicationRepository
{
    public OfferApplicationRepository(JuniorHubContext dbContext, IMapper mapper)
        : base(dbContext, mapper)
    {
    }

    public async Task<bool> ApplicationOfferExistsAsync(int freelancerId, int offerId)
    {
        return await _dbContext.OfferApplications
                        .AnyAsync(v => v.FreelancerId == freelancerId &&
                                       v.OfferId == offerId);
    }

    public async Task<bool> FreelancerApplicationExistsAsync(int freelancerId, int applicationId)
    {
        return await _dbContext.OfferApplications
                        .AnyAsync(v => v.Id == applicationId &&
                                       v.FreelancerId == freelancerId); 
    }
}
