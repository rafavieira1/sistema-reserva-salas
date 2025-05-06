namespace MonolitoBackend.Core.Configuration;

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public int ExpirationInHours { get; set; } = 24;
} 