
namespace JuniorHub.Application.Contracts.Persistence;

public interface IOfferApplicationRepository : IGenericRepository<JuniorHub.Domain.Entities.OfferApplication>
{
    Task<bool> ApplicationOfferExistsAsync(int freelancerId, int offerId);
    Task<bool> FreelancerApplicationExistsAsync(int freelancerId, int applicationId);
}
