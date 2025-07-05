using Nucleus.Domain.Enums;

namespace Nucleus.Api.DTOs.Authentication;

/// <summary>
/// Response model for authentication operations.
/// </summary>
public class AuthenticationResponse
{
    /// <summary>
    /// Whether the authentication was successful.
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// The access token if authentication was successful.
    /// </summary>
    public string? AccessToken { get; set; }
    
    /// <summary>
    /// The refresh token if authentication was successful.
    /// </summary>
    public string? RefreshToken { get; set; }
    
    /// <summary>
    /// User information if authentication was successful.
    /// </summary>
    public UserInfo? User { get; set; }
    
    /// <summary>
    /// Error message if authentication failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Creates a successful authentication response.
    /// </summary>
    public static AuthenticationResponse Success(string accessToken, string refreshToken, UserInfo user)
    {
        return new AuthenticationResponse
        {
            IsSuccess = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = user
        };
    }
    
    /// <summary>
    /// Creates a failed authentication response.
    /// </summary>
    public static AuthenticationResponse Failure(string errorMessage)
    {
        return new AuthenticationResponse
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
}

/// <summary>
/// User information for authentication responses.
/// </summary>
public class UserInfo
{
    /// <summary>
    /// User's unique identifier.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// User's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
    
    /// <summary>
    /// User's username.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// User's role.
    /// </summary>
    public UserRole Role { get; set; }
    
    /// <summary>
    /// Whether the user account is active.
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Date and time when the user last logged in.
    /// </summary>
    public DateTime? LastLoginAt { get; set; }
} 