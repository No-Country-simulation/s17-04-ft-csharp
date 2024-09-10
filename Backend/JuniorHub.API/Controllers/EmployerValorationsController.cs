using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs.Valoration;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ValorationDto>> AddEmployerValoration(ValorationToEmployerDto valorationEmployer)
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
}
