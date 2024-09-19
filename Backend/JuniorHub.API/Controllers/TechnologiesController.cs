using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Technology;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JuniorHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TechnologiesController : ControllerBase
{
    private readonly ITechnologyService _service;

    public TechnologiesController(ITechnologyService service)
    {
        _service = service;
    }

    /// <summary>
    /// Adds a new technology to the system.
    /// </summary>
    /// <remarks>
    /// This method allows an admin to add a new technology based on the data provided in the TechnologyAddDto.
    /// </remarks>
    /// <param name="technologyToAdd">The data transfer object containing the necessary information to add the technology.</param>
    /// <response code="201">The technology was successfully created.</response>
    /// <response code="400">Invalid data in the TechnologyAddDto. Returns an "error":"error message".</response>
    /// <returns>Returns an HTTP action result.</returns>
    [HttpPost()]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddTechnology(TechnologyAddDto technologyToAdd)
    {
        var response = await _service.AddTechnology(technologyToAdd);
        if (response.Success)
        {
            return CreatedAtAction(nameof(GetTechnologyById), new { id = response.Data.Id }, response.Data);
        }
        else
        {
            return BadRequest(response);
        }
    }

    /// <summary>
    /// Retrieves a technology by its ID.
    /// </summary>
    /// <remarks>
    /// This method allows authorized users to retrieve the details of a technology using its unique identifier.
    /// </remarks>
    /// <param name="id">The unique identifier of the technology to retrieve.</param>
    /// <response code="200">The technology details were successfully retrieved.</response>
    /// <response code="404">The technology with the specified ID was not found.</response>
    /// <returns>Returns an HTTP action result with the technology details if found, or a 404 status if not found.</returns>
    [HttpGet("{id}")]
    [Authorize(Roles = "Freelancer, Employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetTechnologyById(int id)
    {
        var response = await _service.GetTechnologyById(id);
        if (response.Success)
        {
            return Ok(response);
        }
        else
        {
            return NotFound(response);
        }
    }

    /// <summary>
    /// Retrieves a list of all technologies.
    /// </summary>
    /// <remarks>
    /// This method allows authorized users to retrieve a complete list of available technologies.
    /// </remarks>
    /// <response code="200">The list of technologies was successfully retrieved.</response>
    /// <response code="403">The user does not have the necessary permissions to access this resource.</response>
    /// <returns>Returns an HTTP action result with a list of technologies.</returns>
    [HttpGet()]
    [Authorize(Roles = "Freelancer, Employer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetAllTechnologies()
    {
        return Ok(await _service.GetAllTechnologies());
    }

    /// <summary>
    /// Updates an existing technology by its ID.
    /// </summary>
    /// <remarks>
    /// This method allows an Admin to update the details of a specific technology based on the provided ID and data in the TechnologyUpdateDto.
    /// </remarks>
    /// <param name="id">The unique identifier of the technology to update.</param>
    /// <param name="technologyToUpdate">The data transfer object containing the updated information for the technology.</param>
    /// <response code="204">The technology was successfully updated. No content is returned.</response>
    /// <response code="404">The technology with the specified ID was not found.</response>
    /// <returns>Returns an HTTP action result.</returns>
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateTechnology(int id, TechnologyUpdateDto technologyToUpdate)
    {
        var response = await _service.UpdateTechnology(id, technologyToUpdate);
        return NoContent();
    }

    /// <summary>
    /// Deletes an existing technology by its ID.
    /// </summary>
    /// <remarks>
    /// This method allows an Admin to delete a specific technology based on the provided ID.
    /// </remarks>
    /// <param name="id">The unique identifier of the technology to delete.</param>
    /// <response code="204">The technology was successfully deleted. No content is returned.</response>
    /// <response code="404">The technology with the specified ID was not found.</response>
    /// <returns>Returns an HTTP action result.</returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTechnology(int id)
    {
        var response = await _service.DeleteTechnology(id);
        return NoContent();
    }
}
