using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a user's password is updated.
/// </summary>
public class UserPasswordUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// The user whose password was updated.
    /// </summary>
    public User User { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public UserPasswordUpdatedEvent(User user)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        OccurredOn = DateTime.UtcNow;
    }
} 