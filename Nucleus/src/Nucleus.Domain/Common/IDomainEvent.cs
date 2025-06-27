namespace Nucleus.Domain.Common;

/// <summary>
/// Marker interface for domain events.
/// All domain events should implement this interface.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// The date and time when the domain event occurred.
    /// </summary>
    DateTime OccurredOn { get; }
} 