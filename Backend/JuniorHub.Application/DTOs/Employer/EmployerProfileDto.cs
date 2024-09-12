using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Domain.Enums;
namespace JuniorHub.Application.DTOs.Employer
{
    public class EmployerProfileDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? MediaUrl { get; set; }
        public ValorationEnum ValorationEnum { get; set; }
        public List<OfferGetWhereDto> Offers { get; set; } = null!;
    }
}