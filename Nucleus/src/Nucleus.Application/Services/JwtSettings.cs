namespace Nucleus.Application.Services;

/// <summary>
/// Configuration settings for JWT authentication.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// The secret key used to sign JWT tokens.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;
    
    /// <summary>
    /// The issuer of the JWT token.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;
    
    /// <summary>
    /// The audience of the JWT token.
    /// </summary>
    public string Audience { get; set; } = string.Empty;
    
    /// <summary>
    /// The expiration time in minutes for access tokens.
    /// </summary>
    public int AccessTokenExpirationMinutes { get; set; } = 60;
    
    /// <summary>
    /// The expiration time in days for refresh tokens.
    /// </summary>
    public int RefreshTokenExpirationDays { get; set; } = 7;
} 