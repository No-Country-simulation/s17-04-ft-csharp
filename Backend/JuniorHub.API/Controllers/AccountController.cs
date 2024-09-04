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
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <remarks>
        /// Este método registra un nuevo usuario basado en los datos proporcionados en el DTO de registro.
        /// </remarks>
        /// <param name="registroDto">Objeto que contiene la información necesaria para registrar al usuario.</param>
        /// <response code="200">El usuario se creó con éxito</response>
        /// <response code="400">Informacion del RegistroDto no valida, retorna un "error":"mensaje de error"</response>
        /// <returns>Una acción de resultado HTTP.</returns>
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
        /// Inicia sesión en el sistema.
        /// </summary>
        /// <remarks>
        /// Este método autentica a un usuario basado en los datos proporcionados en el DTO de inicio de sesión.
        /// </remarks>
        /// <param name="loginDto">Objeto que contiene la información necesaria para autenticar al usuario.</param>
        /// <response code="200">Inicio de sesión exitoso, retorna "token":"valor del token"</response>
        /// <response code="400">Información del LoginDto no válida</response>
        /// <returns>Una acción de resultado HTTP.</returns>
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
