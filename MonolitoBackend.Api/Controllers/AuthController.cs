using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Entities;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(new { 
                    message = "Dados inválidos", 
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            var response = await _authService.RegisterAsync(registrationDto);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { 
                message = ex.Message,
                details = "Por favor, verifique os dados e tente novamente."
            });
        }
    }

    /// <summary>
    /// Lista todos os usuários cadastrados
    /// </summary>
    /// <returns>Lista de usuários</returns>
    /// <remarks>
    /// Requer autenticação e permissão de administrador
    /// </remarks>
    /// <response code="200">Retorna a lista de usuários</response>
    /// <response code="401">Não autorizado</response>
    /// <response code="403">Acesso negado</response>
    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        var users = await _authService.GetAllUsersAsync();
        return Ok(users);
    }
} 