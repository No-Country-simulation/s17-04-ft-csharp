using JuniorHub.Application.DTOs.Link;
using JuniorHub.Application.DTOs.Technology;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.DTOs.Freelancer
{
    public class FreelancerUpdateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string? MediaUrl { get; set; }
        public string Description { get; set; }
        public List<LinkDto> Links { get; set; }
        public List<TechnologiesDto> Technologies { get; set; }
    }
}