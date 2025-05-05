namespace MonolitoBackend.Core.Configuration;

public class JwtSettings
{
    public string Secret { get; set; }
    public int ExpirationInHours { get; set; }
} 