using JuniorHub.Domain.Enums;
using JuniorHub.Domain.Entities;

namespace JunioHub.Application.DTOs.Employer
{
    public class EmployersDto
    {
         public int Id { get; set; }
        public ValorationEnum Valoration { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public string UserName { get; set; } = null!; 
        public string UserLastName { get; set; } = null!; 
        public ICollection<JuniorHub.Domain.Entities.Offer> Offers { get; set; } = null!;
        public ICollection<Valoration> Valorations { get; set; } = null!;
    }
}