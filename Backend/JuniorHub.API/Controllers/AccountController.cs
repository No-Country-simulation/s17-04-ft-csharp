using JunioHub.Application.Contracts.Persistence;
using JunioHub.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuniorHub.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController()
        {
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto register)
        {
            var result = await _authService.RegisterAsync(register);
            if(result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(new { Error = result.Errors.First().Description });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto login)
        {
            (IdentityResult identityResult, string? token) result = await _authService.LoginAsync(login);

            if(!result.identityResult.Succeeded)
            {
                return BadRequest(new { Error = result.identityResult.Errors.First().Description });
            }

            return Ok(new { Token = result.token });
        }

        [HttpGet("admin"),Authorize(Policy ="Admin")]
        public string GetAd() 
        {
            return "yes ad";
        }

        [HttpGet("freelance"),Authorize(Policy="Freelancer")]
        public string GetFre()
        {
            return "yes fre";
        }
        [HttpGet("employer"), Authorize(Policy = "Employer")]
        public string Get()
        {
            return "yes em";
        }
    }
}
