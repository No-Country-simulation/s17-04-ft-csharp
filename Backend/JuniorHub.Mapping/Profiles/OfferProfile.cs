using AutoMapper;
using JunioHub.Application.DTOs.OfferDto;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

    public class OfferProfile:Profile
    {
        public OfferProfile()
        {
         CreateMap<OfferAddDto, Offer>();
        }
    }
