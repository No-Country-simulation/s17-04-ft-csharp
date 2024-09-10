using AutoMapper;
using JunioHub.Application.DTOs.Valoration;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class FreelancerValorationProfile : Profile
{
    public FreelancerValorationProfile()
    {
        CreateMap<ValorationToFreelancerDto, FreelancerValoration>();

        CreateMap<FreelancerValoration, ValorationDto>();

        CreateMap<FreelancerValoration, FreelancerValorationDto>();

    }
}