using JuniorHub.Domain.Enums;
using JuniorHub.Domain.Entities;
using JunioHub.Application.DTOs.Valoration;

namespace JunioHub.Application.DTOs.Employer;

public class EmployersDto
{
    public int Id { get; set; }
    public ValorationEnum Valoration { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string UserLastName { get; set; } = null!;
    public ICollection<Offer> Offers { get; set; } = null!;
    public IEnumerable<ValorationDto> Valorations { get; set; } = Enumerable.Empty<ValorationDto>();
}