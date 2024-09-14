using AutoMapper;
using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class FreelancerValorationProfile : Profile
{
    public FreelancerValorationProfile()
    {
        CreateMap<ValorationToFreelancerDto, FreelancerValoration>();

        CreateMap<FreelancerValoration, ValorationAddDto>();

        CreateMap<FreelancerValoration, ValorationResponseDto>()
            .ForMember(dest => dest.Reviewer, opt => opt.MapFrom(src => $"{src.Employer.User.Name} {src.Employer.User.LastName}"));
    }
}