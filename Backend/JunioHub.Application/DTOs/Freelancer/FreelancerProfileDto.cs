using JunioHub.Application.DTOs.Link;
using JunioHub.Application.DTOs.Technology;
using JuniorHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunioHub.Application.DTOs.Freelancer
{
    public class FreelancerProfileDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? MediaUrl { get; set; }
        public string Description { get; set; }
        public ValorationEnum ValorationEnum { get; set; }
        public List<TechnologiesDto> Technologies { get; set; }
        public List<LinkDto> Links { get; set; }
    }
    
}
