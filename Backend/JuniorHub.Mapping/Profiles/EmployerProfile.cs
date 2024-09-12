using AutoMapper;
using JuniorHub.Application.DTOs.Employer;
using JuniorHub.Domain.Entities;
using JuniorHub.Application.DTOs.Offer;

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
