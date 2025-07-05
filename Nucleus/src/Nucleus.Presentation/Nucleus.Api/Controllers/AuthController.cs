using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Application.Services;
using Nucleus.Api.DTOs.Authentication;
using Nucleus.Domain.Entities;

namespace Nucleus.Api.Controllers;

/// <summary>
/// Controller for authentication operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
    }

    /// <summary>
    /// Authenticates a user with email/username and password.
    /// </summary>
    /// <param name="request">Login request containing email/username and password</param>
    /// <returns>Authentication result with tokens if successful</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthenticationResponse), 200)]
    [ProducesResponseType(typeof(AuthenticationResponse), 400)]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(AuthenticationResponse.Failure("Invalid request data"));
        }

        var result = await _authenticationService.AuthenticateAsync(request.Email, request.Password);
        
        if (!result.IsSuccess)
        {
            return BadRequest(AuthenticationResponse.Failure(result.ErrorMessage!));
        }

        var userInfo = MapToUserInfo(result.User!);
        return Ok(AuthenticationResponse.Success(result.AccessToken!, result.RefreshToken!, userInfo));
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="request">Registration request containing user details</param>
    /// <returns>Authentication result with tokens if successful</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthenticationResponse), 200)]
    [ProducesResponseType(typeof(AuthenticationResponse), 400)]
    public async Task<ActionResult<AuthenticationResponse>> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(AuthenticationResponse.Failure("Invalid request data"));
        }

        var result = await _authenticationService.RegisterAsync(
            request.Email, 
            request.Username, 
            request.Password, 
            request.Role);
        
        if (!result.IsSuccess)
        {
            return BadRequest(AuthenticationResponse.Failure(result.ErrorMessage!));
        }

        var userInfo = MapToUserInfo(result.User!);
        return Ok(AuthenticationResponse.Success(result.AccessToken!, result.RefreshToken!, userInfo));
    }

    /// <summary>
    /// Refreshes an access token using a refresh token.
    /// </summary>
    /// <param name="request">Refresh token request</param>
    /// <returns>Authentication result with new tokens if successful</returns>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthenticationResponse), 200)]
    [ProducesResponseType(typeof(AuthenticationResponse), 400)]
    public async Task<ActionResult<AuthenticationResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(AuthenticationResponse.Failure("Invalid request data"));
        }

        var result = await _authenticationService.RefreshTokenAsync(request.RefreshToken);
        
        if (!result.IsSuccess)
        {
            return BadRequest(AuthenticationResponse.Failure(result.ErrorMessage!));
        }

        var userInfo = MapToUserInfo(result.User!);
        return Ok(AuthenticationResponse.Success(result.AccessToken!, result.RefreshToken!, userInfo));
    }

    /// <summary>
    /// Gets the current user's information.
    /// </summary>
    /// <returns>Current user information</returns>
    [HttpGet("me")]
    [Authorize]
    [ProducesResponseType(typeof(UserInfo), 200)]
    [ProducesResponseType(401)]
    public async Task<ActionResult<UserInfo>> GetCurrentUser()
    {
        // Extract user ID from JWT token
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized();
        }

        // Get user from token validation
        var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrWhiteSpace(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return Unauthorized();
        }

        var token = authorizationHeader.Substring("Bearer ".Length);
        var user = await _authenticationService.ValidateTokenAsync(token);
        
        if (user == null)
        {
            return Unauthorized();
        }

        return Ok(MapToUserInfo(user));
    }

    private static UserInfo MapToUserInfo(User user)
    {
        return new UserInfo
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Role = user.Role,
            IsActive = user.IsActive,
            LastLoginAt = user.LastLoginAt
        };
    }
} 