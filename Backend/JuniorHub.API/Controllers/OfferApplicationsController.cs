using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JuniorHub.API.Controllers;

[Route("api")]
[ApiController]
public class OfferApplicationsController : ControllerBase
{
    private readonly IOfferApplicationService _service;

    public OfferApplicationsController(IOfferApplicationService service)
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
    [HttpPost("applications")]
    [Authorize(Roles = "Freelancer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [HttpDelete("applications/{id}")]
    [Authorize(Roles = "Freelancer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
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
    [HttpGet("offers/{offerId}/applications")]
    [Authorize(Roles = "Employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetApplicationsByOfferId(int offerId)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var applications = await _service.GetApplicationsByOfferIdAsync(userId, offerId);

        return Ok(applications);
    }

    /// <summary>
    /// Selects an application for a specific offer and closes the offer.
    /// </summary>
    /// <param name="offerId">The ID of the offer.</param>
    /// <param name="applicationId">The ID of the application to select.</param>
    /// <returns>Confirmation of the selected application and offer closure.</returns>
    /// <response code="200">Application selected and offer closed.</response>
    /// <response code="404">Offer or application not found.</response>
    [HttpPut("offers/{offerId}/applications/{applicationId}/select")]
    [Authorize(Roles = "Employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SelectApplication(int offerId, int applicationId)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        var response = await _service.SelectApplicationAsync(userId, offerId, applicationId);

        return Ok(response);
    }

}
