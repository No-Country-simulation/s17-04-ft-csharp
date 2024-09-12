using JuniorHub.Application.DTOs.Valoration;
using JuniorHub.Domain.Entities;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Employer;

public class EmployerGetByIdDto
{
    public int Id { get; set; }
    public ValorationEnum Valoration { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public IEnumerable<ValorationDto> Valorations { get; set; } = Enumerable.Empty<ValorationDto>();
}