using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a tenant's subscription is updated.
/// </summary>
public class TenantSubscriptionUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// The tenant whose subscription was updated.
    /// </summary>
    public Tenant Tenant { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public TenantSubscriptionUpdatedEvent(Tenant tenant)
    {
        Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        OccurredOn = DateTime.UtcNow;
    }
} 