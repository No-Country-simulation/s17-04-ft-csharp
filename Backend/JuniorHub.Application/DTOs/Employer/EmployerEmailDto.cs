using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuniorHub.Application.DTOs.Employer
{
    public class EmployerEmailDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? MediaUrl { get; set; }
        public ValorationEnum ValorationEnum { get; set; }
    }
}
