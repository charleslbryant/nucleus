using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;

namespace Nucleus.Application.Services;

/// <summary>
/// Interface for authentication operations.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Authenticates a user with email/username and password.
    /// </summary>
    /// <param name="emailOrUsername">User's email address or username</param>
    /// <param name="password">User's password</param>
    /// <returns>Authentication result with tokens if successful</returns>
    Task<AuthenticationResult> AuthenticateAsync(string emailOrUsername, string password);
    
    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="email">User's email address</param>
    /// <param name="username">User's username</param>
    /// <param name="password">User's password</param>
    /// <param name="role">User's role (defaults to Guest)</param>
    /// <returns>Authentication result with tokens if successful</returns>
    Task<AuthenticationResult> RegisterAsync(string email, string username, string password, UserRole role = UserRole.Guest);
    
    /// <summary>
    /// Refreshes an access token using a refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token</param>
    /// <returns>Authentication result with new tokens if successful</returns>
    Task<AuthenticationResult> RefreshTokenAsync(string refreshToken);
    
    /// <summary>
    /// Validates an access token and returns user information.
    /// </summary>
    /// <param name="accessToken">The access token to validate</param>
    /// <returns>User information if token is valid</returns>
    Task<User?> ValidateTokenAsync(string accessToken);
}

/// <summary>
/// Result of authentication operations.
/// </summary>
public class AuthenticationResult
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
    /// The authenticated user if authentication was successful.
    /// </summary>
    public User? User { get; set; }
    
    /// <summary>
    /// Error message if authentication failed.
    /// </summary>
    public string? ErrorMessage { get; set; }
    
    /// <summary>
    /// Creates a successful authentication result.
    /// </summary>
    public static AuthenticationResult Success(string accessToken, string refreshToken, User user)
    {
        return new AuthenticationResult
        {
            IsSuccess = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = user
        };
    }
    
    /// <summary>
    /// Creates a failed authentication result.
    /// </summary>
    public static AuthenticationResult Failure(string errorMessage)
    {
        return new AuthenticationResult
        {
            IsSuccess = false,
            ErrorMessage = errorMessage
        };
    }
} 