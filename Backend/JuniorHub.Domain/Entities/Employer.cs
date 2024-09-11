using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities;

public class Employer
{
    public int Id { get; set; }
    public ValorationEnum Valoration { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public ICollection<Offer> Offers { get; set; }
    public ICollection<FreelancerValoration> FreelancerValorations { get; set; } = new List<FreelancerValoration>();
    public ICollection<EmployerValoration> EmployerValorations { get; set; } = new List<EmployerValoration>();
}
