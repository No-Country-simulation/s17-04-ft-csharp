using AutoMapper;
using JunioHub.Application.DTOs.Freelancer;
using JuniorHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Mapping.Profiles
{
    internal class FreelancerProfile : Profile
    {
        public FreelancerProfile()
        {
            CreateMap<Freelancer,FreelancerDto>();
            CreateMap<Freelancer, FreelancerProfileDto>();
            CreateMap<FreelancerUpdateDto, User>();
            CreateMap<User, FreelancerProfileDto>().ReverseMap();
        }
    }
}
