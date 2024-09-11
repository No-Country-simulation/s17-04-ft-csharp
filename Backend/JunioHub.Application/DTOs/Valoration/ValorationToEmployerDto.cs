using JuniorHub.Domain.Enums;

namespace JunioHub.Application.DTOs.Valoration;

public class ValorationToEmployerDto
{
    public int EmployerId { get; set; }
    public ValorationEnum ValorationValue { get; set; }
}
