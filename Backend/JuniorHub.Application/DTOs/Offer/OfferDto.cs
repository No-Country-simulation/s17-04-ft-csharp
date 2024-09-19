
using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Offer
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public List<TechnologiesDto> Technologies { get; set; }
    }
}