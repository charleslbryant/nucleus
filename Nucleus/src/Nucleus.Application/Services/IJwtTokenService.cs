using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;

namespace Nucleus.Application.Services;

/// <summary>
/// Interface for JWT token operations.
/// </summary>
public interface IJwtTokenService
{
    /// <summary>
    /// Generates an access token for a user.
    /// </summary>
    /// <param name="user">The user to generate a token for</param>
    /// <returns>The generated access token</returns>
    string GenerateAccessToken(User user);
    
    /// <summary>
    /// Generates a refresh token for a user.
    /// </summary>
    /// <param name="user">The user to generate a token for</param>
    /// <returns>The generated refresh token</returns>
    string GenerateRefreshToken(User user);
    
    /// <summary>
    /// Validates an access token and extracts user information.
    /// </summary>
    /// <param name="token">The access token to validate</param>
    /// <returns>User information if valid, null otherwise</returns>
    JwtTokenValidationResult ValidateAccessToken(string token);
    
    /// <summary>
    /// Validates a refresh token and extracts user information.
    /// </summary>
    /// <param name="token">The refresh token to validate</param>
    /// <returns>User information if valid, null otherwise</returns>
    JwtTokenValidationResult ValidateRefreshToken(string token);
}

/// <summary>
/// Result of JWT token validation.
/// </summary>
public class JwtTokenValidationResult
{
    /// <summary>
    /// Whether the token is valid.
    /// </summary>
    public bool IsValid { get; set; }
    
    /// <summary>
    /// The user ID from the token.
    /// </summary>
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// The user's email from the token.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// The user's username from the token.
    /// </summary>
    public string? Username { get; set; }
    
    /// <summary>
    /// The user's role from the token.
    /// </summary>
    public UserRole? Role { get; set; }
    
    /// <summary>
    /// The tenant ID from the token (for SaaS deployments).
    /// </summary>
    public Guid? TenantId { get; set; }
    
    /// <summary>
    /// Error message if validation failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
} 