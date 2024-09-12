using JuniorHub.Application.Contracts.Services;
using JuniorHub.Application.DTOs.Employer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JuniorHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _service;

    public EmployerController(IEmployerService service)
    {
        _service = service;
    }

    /// <summary>
    /// Gets the profile of a employer by their ID.
    /// </summary>
    /// <param name="id">The ID of the employer whose profile to retrieve.</param>
    /// <returns>The profile of the employer with the specified ID.</returns>
    /// <response code="200">The profile of the employer with the specified ID.</response>
    /// <response code="400">
    /// The request was invalid due to validation errors or failed. 
    /// The response could contain a validation error message or a BaseResponse object indicating failure.
    /// </response>
    /// <response code="404">The employer with the specified ID was not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployerProfileDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployerProfileDto>> GetEmployerById(int id)
    {
        var response= await _service.GetProfileEmployer(id);

        if (response.Success)
        {
           return Ok(response.Data);
        }
        else{
            return NotFound(response);
        }
    }

    /// <summary>
    /// Updates the profile of the currently authenticated employer.
    /// </summary>
    /// <param name="EmployerUpdateDto">The data to update for the employer.</param>
    /// <returns>A response indicating the success or failure of the operation.</returns>
    /// <response code="200">The profile was updated successfully.</response>
    /// <response code="400">
    /// The request was invalid due to validation errors or failed. 
    /// The response could contain a validation error message or a BaseResponse object indicating failure.
    /// </response>
    [HttpPut()]
    [Authorize(Roles = "Employer")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateEmployer(EmployerUpdateDto employerUpdateDto)
    {
        var idUser = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var response = await _service.UpdateEmployer(employerUpdateDto,int.Parse(idUser));
        if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response);
            }
    }

    /// <summary>
    /// Gets the profile of the currently authenticated employer.
    /// </summary>
    /// <returns>The profile of the currently authenticated employer.</returns>
    /// <response code="200">The profile of the current employer.</response>
    /// <response code="400">
    /// The request was invalid due to validation errors or failed. 
    /// The response could contain a validation error message or a BaseResponse object indicating failure.
    /// </response>
    [HttpGet("current")]
    [Authorize(Roles = "Employer")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployerProfileDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployerProfileDto>> GetAllEMployers()
    {
        var idUser = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var response= await _service.GetProfileEmployer(int.Parse(idUser));
        
         if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response);
            }
    }

    }
