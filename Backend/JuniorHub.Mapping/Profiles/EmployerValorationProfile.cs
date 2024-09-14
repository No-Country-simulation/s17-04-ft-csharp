using AutoMapper;
using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class EmployerValorationProfile : Profile
{
    public EmployerValorationProfile()
    {
        CreateMap<ValorationToEmployerDto, EmployerValoration>();

        CreateMap<EmployerValoration, ValorationAddDto>();

        CreateMap<EmployerValoration, ValorationResponseDto>()
            .ForMember(dest => dest.Reviewer, opt => opt.MapFrom(src => $"{src.Freelancer.User.Name} {src.Freelancer.User.LastName}"));
    }
}