
using JuniorHub.Application.DTOs.Offer;
namespace JuniorHub.Application.DTOs.Employer;
    public class EmployerAddDto
    {
        public List<OfferAddDto> Offers { get; set; } = null!;
    }