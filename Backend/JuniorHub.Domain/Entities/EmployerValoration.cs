using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities;

public class EmployerValoration
{
    public int Id { get; set; }
    public ValorationEnum ValorationValue { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int EmployerId { get; set; }
    public Employer Employer { get; set; } = null!;
    public int FreelancerId { get; set; }
    public Freelancer Freelancer { get; set; } = null!;
}
