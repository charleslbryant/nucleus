using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a new user is created.
/// </summary>
public class UserCreatedEvent : IDomainEvent
{
    /// <summary>
    /// The user that was created.
    /// </summary>
    public User User { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public UserCreatedEvent(User user)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        OccurredOn = DateTime.UtcNow;
    }
} 