using JuniorHub.Domain.Enums;

namespace JunioHub.Application.DTOs.Valoration;

public class ValorationToFreelancerDto
{
    public int FreelancerId { get; set; }
    public ValorationEnum ValorationValue { get; set; }
}
