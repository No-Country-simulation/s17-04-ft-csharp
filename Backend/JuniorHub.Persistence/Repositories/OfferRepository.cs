using AutoMapper;
using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Offer;
using JuniorHub.Domain.Entities;
using JuniorHub.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Persistence.Repositories
{
    internal class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        public OfferRepository(JuniorHubContext dbContext,IMapper mapper) : base(dbContext,mapper)
        {
        }

        public IQueryable<Offer> GetAllOfferQuery()
        {
            return _dbContext.Offers.Include(o => o.Technologies);
        }

        public async Task<Offer> GetFullOfferAsync(int idOffer)
        {
            return await _dbContext.Offers.Include(o=>o.Technologies).Include(o=>o.Employer).Include(o=>o.Employer.User).FirstOrDefaultAsync(o=>o.Id == idOffer);
        }

    }
}
