using AutoMapper;
using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Utilities;

namespace JuniorHub.Mapping.Profiles;

public class OfferProfile : Profile
{
    public OfferProfile()
    {
        CreateMap<OfferAddDto, Offer>();
        CreateMap<Offer, OfferDto>();
        CreateMap<Offer, OfferGetByIdDto>();
        CreateMap<OfferUpdateDto, Offer>().ReverseMap();

        CreateMap<Offer, OfferGetWhereDto>()
            //.ForMember(dest => dest.EstimatedTime, opt => opt.MapFrom(src => src.EstimatedTime.GetDescription()))
            .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src => src.Technologies));
    }
}