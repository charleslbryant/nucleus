using Nucleus.Domain.Common;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Events;

namespace Nucleus.Domain.Entities;

/// <summary>
/// Represents a user in the system.
/// 
/// Phase 1: Open Source - Single tenant with local authentication
/// Phase 2: SaaS - Multi-tenant with subscription management
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// User's email address. Must be unique across the system.
    /// </summary>
    public string Email { get; private set; } = string.Empty;
    
    /// <summary>
    /// User's display name or username. Must be unique across the system.
    /// </summary>
    public string Username { get; private set; } = string.Empty;
    
    /// <summary>
    /// Hashed password using secure hashing algorithm (e.g., BCrypt).
    /// </summary>
    public string PasswordHash { get; private set; } = string.Empty;
    
    /// <summary>
    /// User's role in the system (Guest, Reviewer, Admin).
    /// </summary>
    public UserRole Role { get; private set; } = UserRole.Guest;
    
    /// <summary>
    /// Whether the user account is active and can log in.
    /// </summary>
    public bool IsActive { get; private set; } = true;
    
    /// <summary>
    /// Date and time when the user last logged in.
    /// </summary>
    public DateTime? LastLoginAt { get; private set; }
    
    /// <summary>
    /// Date and time when the user account was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Date and time when the user account was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    
    // Phase 2: SaaS Extension Properties
    /// <summary>
    /// Tenant ID for multi-tenant SaaS deployments.
    /// Null for open source single-tenant deployments.
    /// </summary>
    public Guid? TenantId { get; private set; }
    
    /// <summary>
    /// Navigation property to tenant (for SaaS deployments).
    /// </summary>
    public virtual Tenant? Tenant { get; private set; }
    
    // Private constructor for Entity Framework
    private User() { }
    
    /// <summary>
    /// Creates a new user with the specified details.
    /// </summary>
    /// <param name="email">User's email address</param>
    /// <param name="username">User's display name</param>
    /// <param name="passwordHash">Hashed password</param>
    /// <param name="role">User's role (defaults to Guest)</param>
    /// <param name="tenantId">Optional tenant ID for SaaS deployments</param>
    public User(string email, string username, string passwordHash, UserRole role = UserRole.Guest, Guid? tenantId = null)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be null or empty", nameof(username));
        
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be null or empty", nameof(passwordHash));
        
        Email = email.Trim().ToLowerInvariant();
        Username = username.Trim();
        PasswordHash = passwordHash;
        Role = role;
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserCreatedEvent(this));
    }
    
    /// <summary>
    /// Updates the user's password hash.
    /// </summary>
    /// <param name="newPasswordHash">New hashed password</param>
    public void UpdatePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password hash cannot be null or empty", nameof(newPasswordHash));
        
        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserPasswordUpdatedEvent(this));
    }
    
    /// <summary>
    /// Updates the user's role.
    /// </summary>
    /// <param name="newRole">New role</param>
    public void UpdateRole(UserRole newRole)
    {
        if (Role == newRole) return;
        
        var oldRole = Role;
        Role = newRole;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserRoleUpdatedEvent(this, oldRole, newRole));
    }
    
    /// <summary>
    /// Updates the user's profile information.
    /// </summary>
    /// <param name="email">New email address</param>
    /// <param name="username">New username</param>
    public void UpdateProfile(string email, string username)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));
        
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be null or empty", nameof(username));
        
        Email = email.Trim().ToLowerInvariant();
        Username = username.Trim();
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserProfileUpdatedEvent(this));
    }
    
    /// <summary>
    /// Records a successful login.
    /// </summary>
    public void RecordLogin()
    {
        LastLoginAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
    
    /// <summary>
    /// Activates the user account.
    /// </summary>
    public void Activate()
    {
        if (IsActive) return;
        
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserActivatedEvent(this));
    }
    
    /// <summary>
    /// Deactivates the user account.
    /// </summary>
    public void Deactivate()
    {
        if (!IsActive) return;
        
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new UserDeactivatedEvent(this));
    }
    
    /// <summary>
    /// Checks if the user has the specified role or higher.
    /// </summary>
    /// <param name="requiredRole">Minimum required role</param>
    /// <returns>True if user has the required role or higher</returns>
    public bool HasRole(UserRole requiredRole)
    {
        return Role >= requiredRole;
    }
    
    /// <summary>
    /// Checks if the user is an administrator.
    /// </summary>
    /// <returns>True if user is an admin</returns>
    public bool IsAdmin() => Role == UserRole.Admin;
    
    /// <summary>
    /// Checks if the user is a reviewer or admin.
    /// </summary>
    /// <returns>True if user is a reviewer or admin</returns>
    public bool IsReviewer() => Role >= UserRole.Reviewer;
} 