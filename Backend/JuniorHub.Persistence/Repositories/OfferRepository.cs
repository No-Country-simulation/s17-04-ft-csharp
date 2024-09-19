using AutoMapper;
using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace JuniorHub.Persistence.Repositories;

internal class OfferRepository : GenericRepository<Offer>, IOfferRepository
{
    public OfferRepository(JuniorHubContext dbContext,IMapper mapper) : base(dbContext,mapper)
    {
    }

    public IQueryable<Offer> GetAllOfferQuery()
    {
        return _dbContext.Offers
            .Include(o => o.Technologies);
    }

    public async Task<Offer> GetFullOfferAsync(int idOffer)
    {
        return await _dbContext.Offers
            .Include(o=>o.Technologies)
            .Include(o=>o.Employer)
            .Include(o=>o.Employer.User)
            .FirstOrDefaultAsync(o=>o.Id == idOffer);
    }

    public async Task<bool> EmployerOfferExistsAsync(int employerId, int offerId)
    {
        return await _dbContext.Offers
                        .AnyAsync(v => v.Id == offerId &&
                                       v.EmployerId == employerId);
    }
}
