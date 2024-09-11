using JuniorHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Contracts.Persistence
{
    public interface IOfferRepository : IGenericRepository<Offer>
    {
        Task<Offer> GetFullOfferAsync(int idOffer);
        IQueryable<Offer> GetAllOfferQuery();
    }
}
