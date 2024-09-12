using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Valoration;

public class FreelancerValorationDto
{
    public int Id { get; set; }
    public ValorationEnum ValorationValue { get; set; }
    public string Reviewer { get; set; }
}
