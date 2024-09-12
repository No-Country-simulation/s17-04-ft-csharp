using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Valoration;

public class ValorationDto
{
    public int Id { get; set; }
    public ValorationEnum ValorationValue { get; set; }
    public DateTime CreatedAt { get; set; }
}
