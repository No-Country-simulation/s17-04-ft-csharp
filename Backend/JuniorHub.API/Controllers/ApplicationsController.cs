using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs.Application;
using JunioHub.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JuniorHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _service;

    public ApplicationsController(IApplicationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Allows a user with the Freelancer role to apply to an offer.
    /// </summary>
    /// <param name="applyOfferDto">The details of the offer the freelancer is applying to.</param>
    /// <returns>Returns a success status if the application was successful.</returns>
    /// <response code="200">Application successfully created.</response>
    /// <response code="400">Bad request, either due to invalid data or because the user has already applied to the offer.</response>
    /// <response code="401">Unauthorized. The user is not authenticated or does not have the correct role.</response>
    [HttpPost]
    [Authorize(Roles = "Freelancer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ApplyToOffer([FromBody] ApplyOfferDto applyOfferDto)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var response = await _service.ApplyToOfferAsync(userId, applyOfferDto);

        return Ok(response);
    }

    /// <summary>
    /// Allows a Freelancer to delete their application for a specific offer.
    /// </summary>
    /// <param name="id">The ID of the application to be deleted.</param>
    /// <returns>Returns a success message if the application was successfully deleted.</returns>
    /// <response code="200">Application successfully deleted.</response>
    /// <response code="400">Bad request, the application could not be deleted due to invalid input or other issues.</response>
    /// <response code="401">Unauthorized. The user is not authenticated or does not have the Freelancer role.</response>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Freelancer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteApplication(int id)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var response = await _service.DeleteApplicationAsync(userId, id);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves a list of applications for a specific offer, including freelancer details.
    /// </summary>
    /// <param name="offerId">The ID of the offer.</param>
    /// <returns>A list of applications with freelancer details.</returns>
    /// <response code="200">Returns the list of applications.</response>
    /// <response code="404">Offer not found.</response>
    [HttpGet("offer/{offerId}")]
    [Authorize("Employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetApplicationsByOfferId(int offerId)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var applications = await _service.GetApplicationsByOfferIdAsync(userId, offerId);

        return Ok(applications);
    }
}
