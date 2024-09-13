using AutoMapper;
using JuniorHub.Application.DTOs.Employer;
using JuniorHub.Domain.Entities;
using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Domain.Utilities;

namespace JuniorHub.Mapping.Profiles;

public class EmployerProfile : Profile
{
    public EmployerProfile()
    {
        CreateMap<EmployerAddDto, Employer>();
        CreateMap<EmployerUpdateDto, Employer>().ReverseMap();
        CreateMap<Employer, EmployerGetByIdDto>();
        CreateMap<Employer, EmployersDto>();
        CreateMap<EmployerUpdateDto, User>();
        CreateMap<User, EmployerUpdateDto>();

        CreateMap<User, EmployerProfileDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.MediaUrl));

        CreateMap<Employer, EmployerProfileDto>()
            .ForMember(dest => dest.ValorationEnum, opt => opt.MapFrom(src => src.Valoration))
            .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers));

        CreateMap<Offer, OfferGetWhereDto>()
            .ForMember(dest => dest.EstimatedTime, opt => opt.MapFrom(src => src.EstimatedTime.GetDescription()))
            .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src => src.Technologies));

    }
}
