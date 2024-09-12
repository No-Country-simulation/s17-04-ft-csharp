using AutoMapper;
using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class EmployerValorationProfile : Profile
{
    public EmployerValorationProfile()
    {
        CreateMap<ValorationToEmployerDto, EmployerValoration>();

        CreateMap<EmployerValoration, ValorationDto>();
    }
}