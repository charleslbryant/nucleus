using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Interfaces;

namespace Nucleus.Application.Services;

/// <summary>
/// Authentication service implementation.
/// </summary>
public class AuthenticationService(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtTokenService jwtTokenService) : IAuthenticationService
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IPasswordHasher _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    private readonly IJwtTokenService _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));

    /// <inheritdoc/>
    public async Task<AuthenticationResult> AuthenticateAsync(string emailOrUsername, string password)
    {
        if (string.IsNullOrWhiteSpace(emailOrUsername))
            return AuthenticationResult.Failure("Email or username is required");
        
        if (string.IsNullOrWhiteSpace(password))
            return AuthenticationResult.Failure("Password is required");

        // Find user by email or username
        User? user = null;
        
        // First try to find by email
        if (IsValidEmail(emailOrUsername))
        {
            user = await _userRepository.GetByEmailAsync(emailOrUsername);
        }
        
        // If not found by email, try by username
        if (user == null)
        {
            user = await _userRepository.GetByUsernameAsync(emailOrUsername);
        }
        
        if (user == null)
            return AuthenticationResult.Failure("Invalid email/username or password");

        // Check if user is active
        if (!user.IsActive)
            return AuthenticationResult.Failure("User account is deactivated");

        // Verify password
        if (!_passwordHasher.VerifyPassword(password, user.PasswordHash))
            return AuthenticationResult.Failure("Invalid email or password");

        // Record login
        user.RecordLogin();
        await _userRepository.UpdateAsync(user);

        // Generate tokens
        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(user);

        return AuthenticationResult.Success(accessToken, refreshToken, user);
    }

    /// <inheritdoc/>
    public async Task<AuthenticationResult> RegisterAsync(string email, string username, string password, UserRole role = UserRole.Guest)
    {
        if (string.IsNullOrWhiteSpace(email))
            return AuthenticationResult.Failure("Email is required");
        
        if (string.IsNullOrWhiteSpace(username))
            return AuthenticationResult.Failure("Username is required");
        
        if (string.IsNullOrWhiteSpace(password))
            return AuthenticationResult.Failure("Password is required");

        // Validate email format
        if (!IsValidEmail(email))
            return AuthenticationResult.Failure("Invalid email format");

        // Validate password strength
        if (password.Length < 8)
            return AuthenticationResult.Failure("Password must be at least 8 characters long");

        // Check if email already exists
        if (await _userRepository.ExistsByEmailAsync(email))
            return AuthenticationResult.Failure("Email is already registered");

        // Check if username already exists
        if (await _userRepository.ExistsByUsernameAsync(username))
            return AuthenticationResult.Failure("Username is already taken");

        // Hash password
        var passwordHash = _passwordHasher.HashPassword(password);

        // Create user
        var user = new User(email, username, passwordHash, role);
        await _userRepository.AddAsync(user);

        // Generate tokens
        var accessToken = _jwtTokenService.GenerateAccessToken(user);
        var refreshToken = _jwtTokenService.GenerateRefreshToken(user);

        return AuthenticationResult.Success(accessToken, refreshToken, user);
    }

    /// <inheritdoc/>
    public async Task<AuthenticationResult> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return AuthenticationResult.Failure("Refresh token is required");

        // Validate refresh token
        var validationResult = _jwtTokenService.ValidateRefreshToken(refreshToken);
        if (!validationResult.IsValid)
            return AuthenticationResult.Failure(validationResult.ErrorMessage ?? "Invalid refresh token");

        // Get user from token
        var user = await _userRepository.GetByIdAsync(validationResult.UserId!.Value);
        if (user == null)
            return AuthenticationResult.Failure("User not found");

        // Check if user is active
        if (!user.IsActive)
            return AuthenticationResult.Failure("User account is deactivated");

        // Generate new tokens
        var newAccessToken = _jwtTokenService.GenerateAccessToken(user);
        var newRefreshToken = _jwtTokenService.GenerateRefreshToken(user);

        return AuthenticationResult.Success(newAccessToken, newRefreshToken, user);
    }

    /// <inheritdoc/>
    public async Task<User?> ValidateTokenAsync(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            return null;

        // Validate access token
        var validationResult = _jwtTokenService.ValidateAccessToken(accessToken);
        if (!validationResult.IsValid)
            return null;

        // Get user from token
        var user = await _userRepository.GetByIdAsync(validationResult.UserId!.Value);
        if (user == null)
            return null;

        // Check if user is active
        if (!user.IsActive)
            return null;

        return user;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
} 