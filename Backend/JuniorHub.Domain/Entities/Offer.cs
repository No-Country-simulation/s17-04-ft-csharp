using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities;

public class Offer
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public EstimatedTime EstimatedTime { get; set; }
    public State State { get; set; }
    public Difficult Difficult { get; set; }
    public int EmployerId { get; set; }
    public Employer Employer { get; set; } = null!;
    public ICollection<OfferApplication> Applications { get; set; }
    public ICollection<Technology> Technologies { get; set; }
}
