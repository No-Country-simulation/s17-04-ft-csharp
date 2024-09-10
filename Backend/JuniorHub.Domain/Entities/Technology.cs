namespace JuniorHub.Domain.Entities;

public class Technology
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Freelancer> Freelancers { get; set; } = null!;
    public ICollection<Offer> Offers { get; set; } = null!;

}
