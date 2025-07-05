using BCrypt.Net;

namespace Nucleus.Application.Services;

/// <summary>
/// BCrypt implementation of password hashing.
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    /// <inheritdoc/>
    public string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
        
        // Use BCrypt with work factor 12 (good balance of security and performance)
        return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
    }
    
    /// <inheritdoc/>
    public bool VerifyPassword(string password, string hash)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
        
        if (string.IsNullOrWhiteSpace(hash))
            throw new ArgumentException("Hash cannot be null or empty", nameof(hash));
        
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
        catch (BCrypt.Net.SaltParseException)
        {
            // Invalid hash format
            return false;
        }
    }
} 