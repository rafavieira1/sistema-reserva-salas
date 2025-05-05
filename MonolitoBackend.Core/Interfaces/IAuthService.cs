using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    Task<AuthResponseDto> RegisterAsync(UserRegistrationDto registrationDto);
    string GenerateJwtToken(User user);
} 