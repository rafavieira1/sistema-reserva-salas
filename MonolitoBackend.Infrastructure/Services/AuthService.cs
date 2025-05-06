using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MonolitoBackend.Core.DTOs;
using MonolitoBackend.Core.Entities;
using MonolitoBackend.Core.Interfaces;
using MonolitoBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MonolitoBackend.Core.Configuration;

namespace MonolitoBackend.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AuthService(ApplicationDbContext context, JwtSettings jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Credenciais inválidas");
        }

        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Email = user.Email,
            Name = user.Name,
            Role = user.Role
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(UserRegistrationDto registrationDto)
    {
        // Validação de email
        if (string.IsNullOrWhiteSpace(registrationDto.Email))
        {
            throw new InvalidOperationException("O email é obrigatório");
        }

        // Validação de senha
        if (string.IsNullOrWhiteSpace(registrationDto.Password))
        {
            throw new InvalidOperationException("A senha é obrigatória");
        }

        if (registrationDto.Password.Length < 6)
        {
            throw new InvalidOperationException("A senha deve ter pelo menos 6 caracteres");
        }

        // Validação de nome
        if (string.IsNullOrWhiteSpace(registrationDto.Name))
        {
            throw new InvalidOperationException("O nome é obrigatório");
        }

        // Validação de role
        if (string.IsNullOrWhiteSpace(registrationDto.Role))
        {
            throw new InvalidOperationException("A função (role) é obrigatória");
        }

        // Validação de email único
        if (await _context.Users.AnyAsync(u => u.Email.ToLower() == registrationDto.Email.ToLower()))
        {
            throw new InvalidOperationException($"O email '{registrationDto.Email}' já está cadastrado. Por favor, use outro email ou faça login.");
        }

        var user = new User
        {
            Name = registrationDto.Name,
            Email = registrationDto.Email.ToLower(), // Normaliza o email para minúsculas
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password),
            Role = registrationDto.Role
        };

        try
        {
            // Primeiro tenta gerar o token
            var token = GenerateJwtToken(user);

            // Se o token foi gerado com sucesso, salva o usuário
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role
            };
        }
        catch (Exception ex)
        {
            // Se houver qualquer erro, não salva o usuário
            throw new InvalidOperationException($"Erro ao registrar usuário: {ex.Message}", ex);
        }
    }

    public string GenerateJwtToken(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "Usuário não pode ser nulo");
        }

        if (_jwtSettings == null)
        {
            throw new InvalidOperationException("Configurações do JWT não foram inicializadas corretamente");
        }

        if (string.IsNullOrWhiteSpace(_jwtSettings.Secret))
        {
            throw new InvalidOperationException("JWT Secret não configurado corretamente");
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret.Trim());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpirationInHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Erro ao gerar token JWT: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
} 