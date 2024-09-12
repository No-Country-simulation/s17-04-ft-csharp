
namespace JunioHub.Application.Contracts.Persistence;

public interface IApplicationRepository : IGenericRepository<JuniorHub.Domain.Entities.Application>
{
    Task<bool> ApplicationOfferExistsAsync(int freelancerId, int offerId);
    Task<bool> FreelancerApplicationExistsAsync(int freelancerId, int applicationId);
}
