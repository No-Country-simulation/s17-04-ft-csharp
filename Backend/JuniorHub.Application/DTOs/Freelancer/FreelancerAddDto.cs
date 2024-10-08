﻿using JuniorHub.Application.DTOs.Link;
using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.DTOs.Freelancer
{
    public class FreelancerAddDto
    {
        public string Description { get; set; }
        public List<TechnologyAddDto> Technologies { get; set; }
        public List<LinkAddDto> Links { get; set; }
    }
}
