using System.ComponentModel.DataAnnotations;

namespace Nucleus.Api.DTOs.Authentication;

/// <summary>
/// Request model for token refresh.
/// </summary>
public class RefreshTokenRequest
{
    /// <summary>
    /// The refresh token.
    /// </summary>
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
} 