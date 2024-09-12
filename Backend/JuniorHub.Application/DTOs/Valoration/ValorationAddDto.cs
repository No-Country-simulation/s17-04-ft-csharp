using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Valoration;

public class ValorationAddDto
{
    public int FreelancerId { get; set; }
    public int EmployerId { get; set; }
    public ValorationEnum ValorationValue { get; set; }
}
