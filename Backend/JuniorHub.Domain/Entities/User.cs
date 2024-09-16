using Microsoft.AspNetCore.Identity;

namespace JuniorHub.Domain.Entities;

public class User : IdentityUser<int>
{
    public string Name { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? MediaUrl { get; set; }
}
