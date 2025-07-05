using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nucleus.Application.Services;

/// <summary>
/// JWT token service implementation.
/// </summary>
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly SymmetricSecurityKey _signingKey;

    public JwtTokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
        
        if (string.IsNullOrWhiteSpace(_jwtSettings.SecretKey))
            throw new InvalidOperationException("JWT SecretKey is not configured");
        
        _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
    }

    /// <inheritdoc/>
    public string GenerateAccessToken(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var claims = CreateClaims(user);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <inheritdoc/>
    public string GenerateRefreshToken(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var claims = CreateClaims(user);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <inheritdoc/>
    public JwtTokenValidationResult ValidateAccessToken(string token)
    {
        return ValidateToken(token, true);
    }

    /// <inheritdoc/>
    public JwtTokenValidationResult ValidateRefreshToken(string token)
    {
        return ValidateToken(token, false);
    }

    private JwtTokenValidationResult ValidateToken(string token, bool isAccessToken)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return new JwtTokenValidationResult
            {
                IsValid = false,
                ErrorMessage = "Token is null or empty"
            };
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;

            // Extract claims
            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var emailClaim = principal.FindFirst(ClaimTypes.Email)?.Value;
            var usernameClaim = principal.FindFirst(ClaimTypes.Name)?.Value;
            var roleClaim = principal.FindFirst(ClaimTypes.Role)?.Value;
            var tenantIdClaim = principal.FindFirst("tenant_id")?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return new JwtTokenValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid user ID in token"
                };
            }

            if (!Enum.TryParse<UserRole>(roleClaim, out var role))
            {
                return new JwtTokenValidationResult
                {
                    IsValid = false,
                    ErrorMessage = "Invalid role in token"
                };
            }

            Guid? tenantId = null;
            if (!string.IsNullOrWhiteSpace(tenantIdClaim) && Guid.TryParse(tenantIdClaim, out var parsedTenantId))
            {
                tenantId = parsedTenantId;
            }

            return new JwtTokenValidationResult
            {
                IsValid = true,
                UserId = userId,
                Email = emailClaim,
                Username = usernameClaim,
                Role = role,
                TenantId = tenantId
            };
        }
        catch (SecurityTokenExpiredException)
        {
            return new JwtTokenValidationResult
            {
                IsValid = false,
                ErrorMessage = "Token has expired"
            };
        }
        catch (SecurityTokenInvalidSignatureException)
        {
            return new JwtTokenValidationResult
            {
                IsValid = false,
                ErrorMessage = "Invalid token signature"
            };
        }
        catch (Exception ex)
        {
            return new JwtTokenValidationResult
            {
                IsValid = false,
                ErrorMessage = $"Token validation failed: {ex.Message}"
            };
        }
    }

    private static IEnumerable<Claim> CreateClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        // Add tenant ID if present (for SaaS deployments)
        if (user.TenantId.HasValue)
        {
            claims.Add(new Claim("tenant_id", user.TenantId.Value.ToString()));
        }

        return claims;
    }
} 