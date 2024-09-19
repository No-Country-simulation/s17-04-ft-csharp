using JuniorHub.Application.DTOs.Link;
using JuniorHub.Application.DTOs.Technology;
using JuniorHub.Domain.Enums;

namespace JuniorHub.Application.DTOs.Freelancer;

public class FreelancerDto
{
    public string Description { get; set; }
    public List<TechnologiesDto> Technologies { get; set; }
    public ValorationEnum Valoration { get; set; }
    public List<LinkDto> Links { get; set; }
}
