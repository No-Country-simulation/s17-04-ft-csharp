using AutoMapper;
using JuniorHub.Application.DTOs.Identity;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}
