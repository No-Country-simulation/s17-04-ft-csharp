using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Offer;

public class OfferGetWhereDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string EstimatedTime { get; set; }
    public State State { get; set; }
    public Difficult Difficult { get; set; }
    public ICollection<TechnologiesDto> Technologies { get; set; }
}