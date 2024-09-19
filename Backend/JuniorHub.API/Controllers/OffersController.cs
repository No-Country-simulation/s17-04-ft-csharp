using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JuniorHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OffersController : ControllerBase
{
    private readonly IOfferService _offerService;
    public OffersController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    /// <summary>
    /// Adds a new offer.
    /// </summary>
    /// <remarks>
    /// This endpoint allows an employer to create a new offer using the data provided in the OfferAddDto object.
    /// </remarks>
    /// <param name="offerAddDto">The data needed to create a new offer.</param>
    /// <response code="200">The offer was successfully created and its details are returned.</response>
    /// <response code="400">The offer data is invalid. The error message is returned in the response.</response>
    /// <returns>An HTTP action result.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferGetByIdDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost, Authorize(Roles = "Employer")]
    public async Task<ActionResult> AddOffer(OfferAddDto offerAddDto)
    {
        var idUser = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var response = await _offerService.AddOffer(offerAddDto, int.Parse(idUser));
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response.Data);
    }

    /// <summary>
    /// Updates an existing offer.
    /// </summary>
    /// <remarks>
    /// This endpoint allows an employer to update an existing offer by providing the new data and the offer ID.
    /// </remarks>
    /// <param name="idOffer">The ID of the offer to be updated.</param>
    /// <param name="offerUpdateDto">The updated offer data.</param>
    /// <response code="200">The offer was successfully updated and the updated data is returned.</response>
    /// <response code="400">The updated offer data is invalid or the offer could not be found.</response>
    /// <returns>An HTTP action result.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferUpdateDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPut("{idOffer}"), Authorize(Roles = "Employer")]
    public async Task<ActionResult> UpdateOffer(int idOffer, OfferUpdateDto offerUpdateDto)
    {
        var idUser = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var response = await _offerService.UpdateOffer(offerUpdateDto, idOffer, int.Parse(idUser));
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response.Data);
    }
    /*
    /// <summary>
    /// Returns a list of offers.
    /// </summary>
    /// <remarks>
    /// This endpoint allows a freelancer to search for offers by title, technology, and pagination.
    /// </remarks>
    /// <param name="title">Optional. The title of the offer to filter by.</param>
    /// <param name="technology">Optional. The technology name used in the offer to filter by.</param>
    /// <param name="page">Optional. The page number for pagination. Defaults to 1.</param>
    /// <response code="200">A list of offers matching the search criteria is returned.</response>
    /// <response code="400">The search criteria is invalid.</response>
    /// <returns>An HTTP action result.</returns>
    //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OffersPagedDto))]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[HttpGet(), Authorize(Roles = "Freelancer")]
    //public async Task<ActionResult> GetOffers(string title = null, string technology = null, int page = 1)
    //{
    //    var response = await _offerService.GetOffers(title, technology, page);
    //    if (!response.Success)
    //    {
    //        return BadRequest(response);
    //    }
    //    return Ok(response.Data);
    //}
    */

    /// <summary>
    /// Returns a list of offers.
    /// </summary>
    /// <remarks>
    /// This endpoint allows a freelancer to search for offers by title, technology, and pagination.
    /// </remarks>
    /// <param name="search">Optional. The search of the offer to filter by title or technology.</param>
    /// <param name="page">Optional. The page number for pagination. Defaults to 1.</param>
    /// <response code="200">A list of offers matching the search criteria is returned.</response>
    /// <response code="400">The search criteria is invalid.</response>
    /// <returns>An HTTP action result.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OffersPagedDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(), Authorize(Roles = "Freelancer")]
    public async Task<ActionResult> GetOffers(string? search = null, int page = 1)
    {
        var response = await _offerService.GetOffers(search, page);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response.Data);
    }

    /// <summary>
    /// Returns a specific offer by ID.
    /// </summary>
    /// <remarks>
    /// This endpoint allows a freelancer to retrieve the details of a specific offer by providing its ID.
    /// </remarks>
    /// <param name="idOffer">The ID of the offer to be retrieved.</param>
    /// <response code="200">The offer details are returned.</response>
    /// <response code="400">The offer with the given ID could not be found or an error occurred.</response>
    /// <returns>An HTTP action result.</returns>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OfferGetByIdDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("{idOffer}"), Authorize(Roles = "Freelancer")]
    public async Task<ActionResult> GetOffers(int idOffer)
    {
        var response = await _offerService.GetOffer(idOffer);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response.Data);
    }

    /// <summary>
    /// Deletes an existing offer.
    /// </summary>
    /// <remarks>
    /// This endpoint allows an employer to delete an offer by providing its ID.
    /// </remarks>
    /// <param name="idOffer">The ID of the offer to be deleted.</param>
    /// <response code="200">The offer was successfully deleted.</response>
    /// <response code="400">The offer could not be deleted, possibly due to dependencies or incorrect ID.</response>
    /// <returns>An HTTP action result.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{idOffer}"), Authorize(Roles = "Employer")]
    public async Task<ActionResult> DeleteOffer(int idOffer)
    {
        var idUser = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var response = await _offerService.DeleteOffer(idOffer, int.Parse(idUser));
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok();
    }
}
