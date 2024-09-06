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
    public class FreelancerDto
    {
        public string Description { get; set; }
        public List<TechnologiesDto> Technologies { get; set; }
        public ValorationEnum Valoration { get; set; }
        public List<LinkDto> Links { get; set; }
    }
}
