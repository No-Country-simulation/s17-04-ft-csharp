using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Application;

namespace JuniorHub.Application.Contracts.Services;

public interface IOfferApplicationService
{
    Task<BaseResponse<bool>> ApplyToOfferAsync(int freelancerId, ApplyOfferDto applyOfferDto);
    Task<BaseResponse<bool>> DeleteApplicationAsync(int userId, int applicationId);
    Task<BaseResponse<IEnumerable<ApplicationByOfferDto?>>> GetApplicationsByOfferIdAsync(int userId, int offerId);
}
