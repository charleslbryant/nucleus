using System.ComponentModel.DataAnnotations;

namespace Nucleus.Api.DTOs.Authentication;

/// <summary>
/// Request model for user login.
/// </summary>
public class LoginRequest
{
    /// <summary>
    /// User's email address or username.
    /// </summary>
    [Required]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's password.
    /// </summary>
    [Required]
    [MinLength(1)]
    public string Password { get; set; } = string.Empty;
} 