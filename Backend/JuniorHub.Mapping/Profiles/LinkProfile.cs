using AutoMapper;
using JunioHub.Application.DTOs.Freelancer;
using JunioHub.Application.DTOs.Link;
using JunioHub.Application.DTOs.Technology;
using JuniorHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Mapping.Profiles
{
    public class LinkProfile : Profile
    {
        public LinkProfile()
        {
            CreateMap<Link,LinkDto>().ReverseMap();
            CreateMap<LinkAddDto, Link>();
        }
    }
}
