using Microsoft.IdentityModel.Tokens;

namespace BuySubs.Common.Options;

public sealed class JwtOptions
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public string? SigningKey { get; set; }
    public SigningCredentials? SigningCredentials { get; set; }
    public DateTime Expiration => IssuedAt.Add(ValidFor);
    public DateTime NotBefore => DateTime.UtcNow;
    public DateTime IssuedAt => DateTime.UtcNow;
    public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
}