using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a user account is deactivated.
/// </summary>
public class UserDeactivatedEvent : IDomainEvent
{
    /// <summary>
    /// The user that was deactivated.
    /// </summary>
    public User User { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public UserDeactivatedEvent(User user)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        OccurredOn = DateTime.UtcNow;
    }
} 