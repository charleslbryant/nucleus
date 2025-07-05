using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when a user's role is updated.
/// </summary>
public class UserRoleUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// The user whose role was updated.
    /// </summary>
    public User User { get; }
    
    /// <summary>
    /// The previous role.
    /// </summary>
    public UserRole OldRole { get; }
    
    /// <summary>
    /// The new role.
    /// </summary>
    public UserRole NewRole { get; }
    
    /// <summary>
    /// The timestamp when the event occurred.
    /// </summary>
    public DateTime OccurredOn { get; }
    
    public UserRoleUpdatedEvent(User user, UserRole oldRole, UserRole newRole)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        OldRole = oldRole;
        NewRole = newRole;
        OccurredOn = DateTime.UtcNow;
    }
} 