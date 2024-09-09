using AutoMapper;
using JunioHub.Application.DTOs.Employer;
using JuniorHub.Domain.Entities;
using JunioHub.Application.DTOs.OfferDto;

namespace JuniorHub.Mapping.Profiles;

    public class EmployerProfile : Profile
    {
        public EmployerProfile()
    {
        CreateMap<EmployerAddDto, Employer>();
        CreateMap<EmployerUpdateDto, Employer>().ReverseMap();
        CreateMap<Employer, EmployerGetByIdDto>();
        CreateMap<Employer, EmployersDto>();
        CreateMap<EmployerUpdateDto, User>();
        CreateMap<User, EmployerUpdateDto>();
        CreateMap<Offer, OfferGetWhereDto>();
    }
    }
