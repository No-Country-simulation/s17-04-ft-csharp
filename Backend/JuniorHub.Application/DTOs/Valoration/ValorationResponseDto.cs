using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Valoration;

public class ValorationResponseDto
{
    public int Id { get; set; }
    public ValorationEnum ValorationValue { get; set; }
    public string Reviewer { get; set; }
    public DateTime CreatedAt { get; set; }
}
