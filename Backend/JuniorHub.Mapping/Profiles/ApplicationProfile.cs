using AutoMapper;
using JunioHub.Application.DTOs.Application;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<ApplyOfferDto, Application>()
            .ForMember(dest => dest.Selected, opt => opt.MapFrom(src => false)) 
            .ForMember(dest => dest.FreelancerId, opt => opt.Ignore()) 
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Application, ApplicationByOfferDto>()
            .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer.User.LastName + ", " + src.Freelancer.User.Name))
            .ForMember(dest => dest.FreelancerDescription, opt => opt.MapFrom(src => src.Freelancer.Description))
            .ForMember(dest => dest.Technologies, opt => opt.MapFrom(src => src.Freelancer.Technologies));
    }
}