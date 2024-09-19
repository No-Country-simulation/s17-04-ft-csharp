using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Identity;

public class RegisterDto
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}
