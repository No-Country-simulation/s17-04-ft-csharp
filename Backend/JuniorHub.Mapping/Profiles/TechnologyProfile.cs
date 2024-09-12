using AutoMapper;
using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class TechnologyProfile : Profile
{
    public TechnologyProfile()
    {
        CreateMap<TechnologyAddDto, Technology>();
        CreateMap<TechnologyUpdateDto, Technology>().ReverseMap();
        CreateMap<Technology, TechnologyGetByIdDto>();
        CreateMap<Technology, TechnologiesDto>().ReverseMap();
    }
}