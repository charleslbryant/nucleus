using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a new tenant is created.
/// </summary>
public class TenantCreatedEvent : IDomainEvent
{
    /// <summary>
    /// The tenant that was created.
    /// </summary>
    public Tenant Tenant { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public TenantCreatedEvent(Tenant tenant)
    {
        Tenant = tenant ?? throw new ArgumentNullException(nameof(tenant));
        OccurredOn = DateTime.UtcNow;
    }
} 