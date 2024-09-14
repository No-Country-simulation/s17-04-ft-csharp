using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Valoration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JuniorHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployerValorationsController : ControllerBase
{
    private readonly IEmployerValorationService _service;

    public EmployerValorationsController(IEmployerValorationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Adds a rating (valoration) for an employer by a freelancer.
    /// </summary>
    /// <remarks>
    /// This method allows a freelancer to rate an employer based on the provided valoration details.
    /// The freelancer must be authorized, and the valoration will be tied to their user ID.
    /// </remarks>
    /// <param name="valorationEmployer">The data transfer object containing the valoration details for the employer.</param>
    /// <response code="200">The valoration was successfully added.</response>
    /// <response code="400">Invalid input data or the operation failed.</response>
    /// <response code="401">The user is not authorized to perform this action.</response>
    /// <returns>Returns an HTTP action result with the valoration details or an error message.</returns>
    [HttpPost()]
    [Authorize(Roles = "Freelancer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValorationAddDto))]
    public async Task<ActionResult<ValorationAddDto>> AddEmployerValoration(ValorationToEmployerDto valorationEmployer)
    {
        var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var response = await _service.AddEmployerValoration(int.Parse(userId), valorationEmployer);
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
    /// Retrieves all valuations for a specific employer.
    /// </summary>
    /// <param name="employerId">The ID of the employer to retrieve valuations for.</param>
    /// <returns>
    /// A list of all valuations associated with the specified employer ID.
    /// If no valuations are found, the response will still indicate success with an empty list.
    /// </returns>
    /// <response code="200">Returns a BaseResponse containing the list of EmployerValorationDto, which includes all the valuations for the specified freelancer.</response>
    /// <response code="400">Invalid input data or the operation failed.</response>
    /// <response code="404">If no valuations are found for the specified employer ID.</response>
    [HttpGet("{employerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValorationResponseDto))]
    public async Task<ActionResult> GetAllValorationsForEmployer(int employerId)
    {
        var response = await _service.GetAllValorationsForEmployerAsync(employerId);

        return Ok(response);
    }
}
