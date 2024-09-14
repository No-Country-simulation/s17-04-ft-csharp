using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Offer;
using JuniorHub.Application.DTOs.Valoration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JuniorHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FreelancerValorationsController : ControllerBase
{
    private readonly IFreelancerValorationService _service;

    public FreelancerValorationsController(IFreelancerValorationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Adds a rating (valoration) for a freelancer by an employer.
    /// </summary>
    /// <remarks>
    /// This method allows an employer to rate a freelancer based on the provided valoration details.
    /// The employer must be authorized and the valoration will be tied to their user ID.
    /// </remarks>
    /// <param name="valorationFreelancer">The data transfer object containing the valoration details for the freelancer.</param>
    /// <response code="200">The valoration was successfully added.</response>
    /// <response code="400">Invalid input data or the operation failed.</response>
    /// <response code="401">The user is not authorized to perform this action.</response>
    /// <returns>Returns an HTTP action result with the valoration details or an error message.</returns>
    [HttpPost()]
    [Authorize(Roles = "Employer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValorationAddDto))]
    public async Task<ActionResult<ValorationAddDto>> AddFreelancerValoration(ValorationToFreelancerDto valorationFreelancer)
    {
        var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var response = await _service.AddFreelancerValoration(int.Parse(userId), valorationFreelancer);
        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Retrieves all valuations for a specific freelancer.
    /// </summary>
    /// <param name="freelancerId">The ID of the freelancer to retrieve valuations for.</param>
    /// <returns>
    /// A list of all valuations associated with the specified freelancer ID.
    /// If no valuations are found, the response will still indicate success with an empty list.
    /// </returns>
    /// <response code="200">Returns a BaseResponse containing the list of FreelancerValorationDto, which includes all the valuations for the specified freelancer.</response>
    /// <response code="400">Invalid input data or the operation failed.</response>
    /// <response code="404">If no valuations are found for the specified freelancer ID.</response>
    [HttpGet("{freelancerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValorationResponseDto))]
    public async Task<ActionResult> GetAllValorationsForFreelancer(int freelancerId)
    {
        var response = await _service.GetAllValorationsForFreelancerAsync(freelancerId);

        return Ok(response);
    }
}
