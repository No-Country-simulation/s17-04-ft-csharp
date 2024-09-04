using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs.Technology;
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

    [HttpPost()]
    //[Authorize(Roles = "Admin")]
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

    [HttpGet("{id}")]
    //[Authorize(Roles = "Freelancer, Employer")]
    public async Task<ActionResult> GetTechnologyById(int id)
    {
        return Ok(await _service.GetTechnologyById(id));
    }

    [HttpGet()]
    //[Authorize(Roles = "Freelancer, Employer")]
    public async Task<ActionResult> GetAllTechnologies()
    {
        return Ok(await _service.GetAllTechnologies());
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateTechnology(int id, TechnologyUpdateDto technologyToUpdate)
    {
        var response = await _service.UpdateTechnology(id, technologyToUpdate);
        return NoContent();
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTechnology(int id)
    {
        var response = await _service.DeleteTechnology(id);
        return NoContent();
    }
}
