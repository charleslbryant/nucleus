using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a tenant is deactivated.
/// </summary>
public class TenantDeactivatedEvent : IDomainEvent
{
    /// <summary>
    /// The tenant that was deactivated.
    /// </summary>
    public Tenant Tenant { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public TenantDeactivatedEvent(Tenant tenant)
    {
        Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        OccurredOn = DateTime.UtcNow;
    }
} 