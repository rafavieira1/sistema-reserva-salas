using MonolitoBackend.Core.Entities;

namespace MonolitoBackend.Core.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    bool ValidateToken(string token);
} 