﻿using JunioHub.Application.Contracts.Services;
using JunioHub.Application.DTOs.Freelancer;
using JunioHub.Application.DTOs.Technology;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JuniorHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FreelancersController : ControllerBase
    {
        private readonly IFreelancerService _freelancerService;

        public FreelancersController(IFreelancerService freelancerService)
        {
            _freelancerService = freelancerService;
        }
        /// <summary>
        /// Adds a new technology for a freelancer.
        /// </summary>
        /// <param name="technologyToAdd">The data for the new technology to add.</param>
        /// <returns>A response indicating the success or failure of the operation.</returns>
        /// <response code="200">The technology was added successfully.</response>
        /// <response code="400">The request was invalid or failed.</response>
        [HttpPost()]
        [Authorize(Roles = "Freelancer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddFreelancer(FreelancerAddDto technologyToAdd)
        {
            var idUser = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var response = await _freelancerService.AddFreelancer(technologyToAdd, int.Parse(idUser));

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
        /// Gets the profile of the currently authenticated freelancer.
        /// </summary>
        /// <returns>The profile of the currently authenticated freelancer.</returns>
        /// <response code="200">The profile of the current freelancer.</response>
        /// <response code="400">The request was invalid or failed.</response>
        [HttpGet("current")]
        [Authorize(Roles = "Freelancer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FreelancerProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FreelancerProfileDto>> GetCurrentFreelancerProfile()
        {
            var idUser = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var response = await _freelancerService.GetProfileFreelancer(int.Parse(idUser));

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
        /// Gets the profile of a freelancer by their ID.
        /// </summary>
        /// <param name="id">The ID of the freelancer whose profile to retrieve.</param>
        /// <returns>The profile of the freelancer with the specified ID.</returns>
        /// <response code="200">The profile of the freelancer with the specified ID.</response>
        /// <response code="404">The freelancer with the specified ID was not found.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FreelancerProfileDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FreelancerProfileDto>> GetFreelancerProfileById(int id)
        {
            var response = await _freelancerService.GetProfileFreelancer(id);

            if (response.Success)
            {
                return Ok(response.Data);
            }
            else
            {
                return NotFound(response);
            }
        }

    }
}