using JuniorHub.Application.Contracts.Persistence;
using JuniorHub.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuniorHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <remarks>
        /// This method registers a new user based on the data provided in the registration DTO.
        /// </remarks>
        /// <param name="registerDto">Object containing the information necessary to register the user.</param>
        /// <response code="200">The user was created successfully.</response>
        /// <response code="400">The registration information is invalid. Returns "error":"error message".</response>
        /// <returns>An HTTP action result.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Tuple<string>))]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registroDto)
        {
            var result = await _authService.RegisterAsync(registroDto);
            if(result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(new { Error = result.Errors.First().Description });
        }


        /// <summary>
        /// Log in to the system.
        /// </summary>
        /// <remarks>
        /// This method authenticates a user based on the data provided in the login DTO.
        /// </remarks>
        /// <param name="loginDto">Object containing the information necessary to authenticate the user.</param>
        /// <response code="200">Login successful. Returns "token":"token value".</response>
        /// <response code="400">The login information is invalid.</response>
        /// <returns>An HTTP action result.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tuple<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Tuple<string>))]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            (IdentityResult identityResult, string? token) result = await _authService.LoginAsync(loginDto);

            if(!result.identityResult.Succeeded)
            {
                return BadRequest(new { Error = result.identityResult.Errors.First().Description });
            }

            return Ok(new { Token = result.token });
        }

    }
}
