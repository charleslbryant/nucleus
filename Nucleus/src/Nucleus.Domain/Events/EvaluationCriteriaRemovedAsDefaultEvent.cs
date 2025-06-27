using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when evaluation criteria is removed as default.
/// </summary>
public class EvaluationCriteriaRemovedAsDefaultEvent : IDomainEvent
{
    public EvaluationCriteria EvaluationCriteria { get; }

    public EvaluationCriteriaRemovedAsDefaultEvent(EvaluationCriteria evaluationCriteria)
    {
        EvaluationCriteria = evaluationCriteria ?? throw new ArgumentNullException(nameof(evaluationCriteria));
    }

    public DateTime OccurredOn => DateTime.UtcNow;
} 