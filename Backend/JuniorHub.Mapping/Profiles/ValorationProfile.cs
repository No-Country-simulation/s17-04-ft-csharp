using AutoMapper;
using JunioHub.Application.DTOs.Valoration;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class ValorationProfile : Profile
{
    public ValorationProfile()
    {
        CreateMap<ValorationToEmployerDto, Valoration>();

        CreateMap<ValorationToFreelancerDto, Valoration>();

        CreateMap<Valoration, ValorationDto>();
    }
}
