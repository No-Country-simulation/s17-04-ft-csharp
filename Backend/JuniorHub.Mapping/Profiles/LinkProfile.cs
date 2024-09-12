using AutoMapper;
using JuniorHub.Application.DTOs.Link;
using JuniorHub.Domain.Entities;

namespace JuniorHub.Mapping.Profiles;

public class LinkProfile : Profile
{
    public LinkProfile()
    {
        CreateMap<Link,LinkDto>().ReverseMap();
        CreateMap<LinkAddDto, Link>();
    }
}
