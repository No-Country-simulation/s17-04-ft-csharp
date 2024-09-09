using JuniorHub.Domain.Enums;

namespace JunioHub.Application.DTOs.Employer
{
    public class EmployerUpdateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? MediaUrl { get; set; }
        public string Description { get; set; }
    }
}