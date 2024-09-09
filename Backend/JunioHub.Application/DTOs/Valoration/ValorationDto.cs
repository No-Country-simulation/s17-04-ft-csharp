using JuniorHub.Domain.Enums;

namespace JunioHub.Application.DTOs.Valoration;

public class ValorationDto
{
    public int Id { get; set; }
    public string Reviewer { get; set; } = null!;
    public ValorationEnum ValorationValue { get; set; }
}
