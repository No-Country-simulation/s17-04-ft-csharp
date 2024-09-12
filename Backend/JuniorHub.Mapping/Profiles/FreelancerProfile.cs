using AutoMapper;
using JuniorHub.Application.DTOs.Freelancer;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

internal class FreelancerProfile : Profile
{
    public FreelancerProfile()
    {
        CreateMap<Freelancer,FreelancerDto>();
        CreateMap<Freelancer, FreelancerProfileDto>();
        CreateMap<FreelancerUpdateDto, User>();
        CreateMap<User, FreelancerProfileDto>().ReverseMap();
    }
}
