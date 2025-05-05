using Microsoft.AspNetCore.Mvc;
using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Interfaces;

namespace MonolitoBackend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Realiza o login do usuário
    /// </summary>
    /// <param name="loginDto">Dados de login</param>
    /// <returns>Token JWT e informações do usuário</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/auth/login
    ///     {
    ///        "email": "usuario@exemplo.com",
    ///        "password": "senha123"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Retorna o token JWT e informações do usuário</response>
    /// <response code="401">Credenciais inválidas</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            var response = await _authService.LoginAsync(loginDto);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Credenciais inválidas" });
        }
    }

    /// <summary>
    /// Registra um novo usuário
    /// </summary>
    /// <param name="registrationDto">Dados de registro</param>
    /// <returns>Token JWT e informações do usuário</returns>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     POST /api/auth/register
    ///     {
    ///        "name": "Usuário Exemplo",
    ///        "email": "usuario@exemplo.com",
    ///        "password": "senha123",
    ///        "role": "User"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Retorna o token JWT e informações do usuário</response>
    /// <response code="400">Email já cadastrado ou dados inválidos</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody] UserRegistrationDto registrationDto)
    {
        try
        {
            var response = await _authService.RegisterAsync(registrationDto);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 