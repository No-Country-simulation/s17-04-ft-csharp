using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Application;

namespace JunioHub.Application.Contracts.Services;

public interface IApplicationService
{
    Task<BaseResponse<bool>> ApplyToOfferAsync(int freelancerId, ApplyOfferDto applyOfferDto);
    Task<BaseResponse<bool>> DeleteApplicationAsync(int userId, int applicationId);
    Task<BaseResponse<IEnumerable<ApplicationByOfferDto?>>> GetApplicationsByOfferIdAsync(int userId, int offerId);
}
