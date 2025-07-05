using Nucleus.Domain.Common;
using Nucleus.Domain.Events;

namespace Nucleus.Domain.Entities;

/// <summary>
/// Represents a tenant in a multi-tenant SaaS deployment.
/// This entity is used for Phase 2 SaaS extension and is not used in open source deployments.
/// </summary>
public class Tenant : BaseEntity
{
    /// <summary>
    /// The name of the tenant/organization.
    /// </summary>
    public string Name { get; private set; } = string.Empty;
    
    /// <summary>
    /// The subdomain for the tenant (e.g., "acme" for acme.nucleus.com).
    /// </summary>
    public string Subdomain { get; private set; } = string.Empty;
    
    /// <summary>
    /// Whether the tenant is active and can access the system.
    /// </summary>
    public bool IsActive { get; private set; } = true;
    
    /// <summary>
    /// The subscription plan for the tenant.
    /// </summary>
    public string SubscriptionPlan { get; private set; } = "free";
    
    /// <summary>
    /// Date when the subscription expires.
    /// </summary>
    public DateTime? SubscriptionExpiresAt { get; private set; }
    
    /// <summary>
    /// The date and time when the tenant was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    /// <summary>
    /// The date and time when the tenant was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Navigation property to users belonging to this tenant.
    /// </summary>
    public virtual ICollection<User> Users { get; private set; } = new List<User>();
    
    // Private constructor for Entity Framework
    private Tenant() { }
    
    /// <summary>
    /// Creates a new tenant with the specified details.
    /// </summary>
    /// <param name="name">Tenant name</param>
    /// <param name="subdomain">Tenant subdomain</param>
    /// <param name="subscriptionPlan">Subscription plan (defaults to "free")</param>
    public Tenant(string name, string subdomain, string subscriptionPlan = "free")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(subdomain))
            throw new ArgumentException("Subdomain cannot be null or empty", nameof(subdomain));
        
        Name = name.Trim();
        Subdomain = subdomain.Trim().ToLowerInvariant();
        SubscriptionPlan = subscriptionPlan.Trim();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new TenantCreatedEvent(this));
    }
    
    /// <summary>
    /// Updates the tenant's subscription plan.
    /// </summary>
    /// <param name="plan">New subscription plan</param>
    /// <param name="expiresAt">When the subscription expires</param>
    public void UpdateSubscription(string plan, DateTime? expiresAt = null)
    {
        if (string.IsNullOrWhiteSpace(plan))
            throw new ArgumentException("Plan cannot be null or empty", nameof(plan));
        
        SubscriptionPlan = plan.Trim();
        SubscriptionExpiresAt = expiresAt;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new TenantSubscriptionUpdatedEvent(this));
    }
    
    /// <summary>
    /// Activates the tenant.
    /// </summary>
    public void Activate()
    {
        if (IsActive) return;
        
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new TenantActivatedEvent(this));
    }
    
    /// <summary>
    /// Deactivates the tenant.
    /// </summary>
    public void Deactivate()
    {
        if (!IsActive) return;
        
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new TenantDeactivatedEvent(this));
    }
    
    /// <summary>
    /// Checks if the tenant's subscription is active.
    /// </summary>
    /// <returns>True if subscription is active</returns>
    public bool IsSubscriptionActive()
    {
        if (!IsActive) return false;
        if (SubscriptionPlan == "free") return true;
        return SubscriptionExpiresAt == null || SubscriptionExpiresAt > DateTime.UtcNow;
    }
} 