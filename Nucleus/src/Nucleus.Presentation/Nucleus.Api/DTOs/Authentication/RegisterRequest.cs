using System.ComponentModel.DataAnnotations;
using Nucleus.Domain.Enums;

namespace Nucleus.Api.DTOs.Authentication;

/// <summary>
/// Request model for user registration.
/// </summary>
public class RegisterRequest
{
    /// <summary>
    /// User's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's username.
    /// </summary>
    [Required]
    [MinLength(3)]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// User's password.
    /// </summary>
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// User's role (optional, defaults to Guest).
    /// </summary>
    public UserRole Role { get; set; } = UserRole.Guest;
} 