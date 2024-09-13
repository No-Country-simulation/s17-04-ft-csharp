using JuniorHub.Application.DTOs;
using JuniorHub.Application.DTOs.Offer;

namespace JuniorHub.Application.Contracts.Services;

public interface IOfferService
{
    Task<BaseResponse<OfferGetByIdDto>> AddOffer(OfferAddDto offerAddDto, int idUser);
    Task<BaseResponse<OfferUpdateDto>> UpdateOffer(OfferUpdateDto offerUpdateDto,int idOffer,int idUser);
    Task<BaseResponse<OfferGetByIdDto>> GetOffer(int idOffer);
    Task<BaseResponse<OffersPagedDto>> GetOffers(string? title, string? technology, int page);
    Task<BaseResponse<OffersPagedDto>> GetOffers(string? search, int page);
    Task<BaseResponse<bool>> DeleteOffer(int idOffer, int idUser);
}
