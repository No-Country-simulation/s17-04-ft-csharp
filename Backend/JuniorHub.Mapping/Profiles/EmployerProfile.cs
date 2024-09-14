using AutoMapper;
using JuniorHub.Application.DTOs.Employer;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class EmployerProfile : Profile
{
    public EmployerProfile()
    {
        CreateMap<EmployerUpdateDto, Employer>().ReverseMap();
        CreateMap<EmployerUpdateDto, User>();
        CreateMap<User, EmployerUpdateDto>();

        CreateMap<Employer, EmployerProfileDto>()
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name)) 
          .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName)) 
          .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email)) 
          .ForMember(dest => dest.MediaUrl, opt => opt.MapFrom(src => src.User.MediaUrl)) 
          .ForMember(dest => dest.ValorationEnum, opt => opt.MapFrom(src => src.Valoration)) 
          .ForMember(dest => dest.Offers, opt => opt.MapFrom(src => src.Offers)); 

    }
}
