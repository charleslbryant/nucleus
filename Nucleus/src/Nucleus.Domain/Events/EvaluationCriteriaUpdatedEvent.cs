using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when evaluation criteria is updated.
/// </summary>
public class EvaluationCriteriaUpdatedEvent : IDomainEvent
{
    public EvaluationCriteria EvaluationCriteria { get; }

    public EvaluationCriteriaUpdatedEvent(EvaluationCriteria evaluationCriteria)
    {
        EvaluationCriteria = evaluationCriteria ?? throw new ArgumentNullException(nameof(evaluationCriteria));
    }

    public DateTime OccurredOn => DateTime.UtcNow;
} 