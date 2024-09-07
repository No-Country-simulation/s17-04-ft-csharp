using JuniorHub.Domain.Enums;

namespace JuniorHub.Domain.Entities;

public class Valoration
{
    public int Id { get; set; }
    public ValorationEnum ValorationValue { get; set; }
    public string Reviewer { get; set; } = null!;
    public int FreelancerId { get; set; }
    public Freelancer Freelancer { get; set; } = null!;
    public int EmployerId { get; set; }
    public Employer Employer { get; set; } = null!;
}
