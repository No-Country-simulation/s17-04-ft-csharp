using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities;

public class Freelancer
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public ValorationEnum Valoration { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public ICollection<Technology> Technologies { get; set; } = null!;
    public ICollection<Link> Links { get; set; } = null!;
    public ICollection<Application> Applications { get; set; } = null!;
    public ICollection<Valoration> Valorations { get; set; } = null!;
}
