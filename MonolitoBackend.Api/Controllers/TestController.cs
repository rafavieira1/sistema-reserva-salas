using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Core.Exceptions;
using System.Security.Claims;

namespace MonolitoBackend.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public TestController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    [HttpGet("public")]
    public IActionResult PublicEndpoint()
    {
        return Ok(new { message = "Endpoint público acessível" });
    }

    [Authorize]
    [HttpGet("private")]
    public IActionResult PrivateEndpoint()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(new { message = "Endpoint privado acessível", userId });
    }

    [HttpGet("test-exception")]
    public IActionResult TestException()
    {
        throw new UnauthorizedAccessException("Teste de exceção não autorizada");
    }

    [HttpGet("test-validation")]
    public IActionResult TestValidation()
    {
        throw new ValidationException("Teste de exceção de validação");
    }

    [HttpGet("test-not-found")]
    public IActionResult TestNotFound()
    {
        throw new KeyNotFoundException("Teste de recurso não encontrado");
    }

    [HttpGet("test-invalid-operation")]
    public IActionResult TestInvalidOperation()
    {
        throw new InvalidOperationException("Teste de operação inválida");
    }

    [HttpGet("test-not-supported")]
    public IActionResult TestNotSupported()
    {
        throw new NotSupportedException("Teste de operação não suportada");
    }

    [HttpGet("test-error")]
    public IActionResult TestError()
    {
        throw new Exception("Teste de erro interno");
    }
} 