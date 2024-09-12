using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Employer
{
    public class EmployerUpdateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? MediaUrl { get; set; }
    }
}