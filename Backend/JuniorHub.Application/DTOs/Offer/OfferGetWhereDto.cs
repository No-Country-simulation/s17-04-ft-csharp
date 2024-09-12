using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Offer
{
    public class OfferGetWhereDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime EstimatedTime { get; set; }
        public State State { get; set; }
        public Difficult Difficult { get; set; }
    }
}