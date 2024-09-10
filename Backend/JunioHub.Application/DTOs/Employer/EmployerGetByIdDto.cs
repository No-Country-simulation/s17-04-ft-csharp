using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JuniorHub.Domain.Enums;
using JuniorHub.Domain.Entities;
namespace JunioHub.Application.DTOs.Employer;
    public class EmployerGetByIdDto
    {
         public int Id { get; set; }
        public ValorationEnum Valoration { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Valoration> Valorations { get; set; } = null!;
    }