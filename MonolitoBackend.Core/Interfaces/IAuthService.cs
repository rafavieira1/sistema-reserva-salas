using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
    string GenerateJwtToken(User user);
    Task<IEnumerable<User>> GetAllUsersAsync();
} 