using System.ComponentModel.DataAnnotations;

namespace MonolitoBackend.Core.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class UserRegistrationDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }
}

public class AuthResponseDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
} 