using AutoMapper;
using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class OfferProfile : Profile
{
    public OfferProfile()
    {
        CreateMap<OfferAddDto, Offer>();
        CreateMap<Offer, OfferDto>();
        CreateMap<Offer, OfferGetByIdDto>();
        CreateMap<OfferUpdateDto, Offer>().ReverseMap();
    }
}