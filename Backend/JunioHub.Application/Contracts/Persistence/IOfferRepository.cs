using JuniorHub.Domain.Entities;

namespace JunioHub.Application.Contracts.Persistence;

public interface IOfferRepository : IGenericRepository<Offer>
{
    Task<Offer> GetFullOfferAsync(int idOffer);
    IQueryable<Offer> GetAllOfferQuery();
    Task<bool> EmployerOfferExistsAsync(int employerId, int offerId);
}
