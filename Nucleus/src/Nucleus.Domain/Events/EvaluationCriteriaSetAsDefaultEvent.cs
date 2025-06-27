using Nucleus.Domain.Common;
using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Events;

/// <summary>
/// Domain event raised when evaluation criteria is set as default.
/// </summary>
public class EvaluationCriteriaSetAsDefaultEvent : IDomainEvent
{
    public EvaluationCriteria EvaluationCriteria { get; }

    public EvaluationCriteriaSetAsDefaultEvent(EvaluationCriteria evaluationCriteria)
    {
        EvaluationCriteria = evaluationCriteria ?? throw new ArgumentNullException(nameof(evaluationCriteria));
    }

    public DateTime OccurredOn => DateTime.UtcNow;
} 