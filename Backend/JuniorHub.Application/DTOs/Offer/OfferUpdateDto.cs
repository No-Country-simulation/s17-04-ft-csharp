
using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Offer
{
    public class OfferUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime EstimatedTime { get; set; }
        public State State { get; set; }
        public Difficult Difficult { get; set; }
        public decimal Price { get; set; }
        public List<TechnologiesDto> Technologies { get; set; }
    }
}