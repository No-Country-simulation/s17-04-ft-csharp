
using JunioHub.Application.DTOs.OfferDto;
namespace JunioHub.Application.DTOs.Employer;
    public class EmployerAddDto
    {
        public List<OfferAddDto> Offers { get; set; } = null!;
    }