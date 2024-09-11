using JunioHub.Application.DTOs;
using JunioHub.Application.DTOs.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.Contracts.Services
{
    public interface IOfferService
    {
        Task<BaseResponse<OfferGetByIdDto>> AddOffer(OfferAddDto offerAddDto, int idUser);
        Task<BaseResponse<OfferUpdateDto>> UpdateOffer(OfferUpdateDto offerUpdateDto,int idOffer,int idUser);
        Task<BaseResponse<OfferGetByIdDto>> GetOffer(int idOffer);
        Task<BaseResponse<OffersPagedDto>> GetOffers(string? title, string? technology, int page);
        Task<BaseResponse<bool>> DeleteOffer(int idOffer, int idUser);
    }
}
