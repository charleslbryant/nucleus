using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a user's profile is updated.
/// </summary>
public class UserProfileUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// The user whose profile was updated.
    /// </summary>
    public User User { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public UserProfileUpdatedEvent(User user)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        OccurredOn = DateTime.UtcNow;
    }
} 