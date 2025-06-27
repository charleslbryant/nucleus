using Nucleus.Domain.Common;

namespace Nucleus.Domain.Entities;

/// <summary>
/// Represents an evaluation of a model run output.
/// </summary>
public class Evaluation : BaseEntity
{
    /// <summary>
    /// The model run this evaluation belongs to.
    /// </summary>
    public Guid ModelRunId { get; private set; }

    /// <summary>
    /// The type of evaluator (llm, human, rule).
    /// </summary>
    public string EvaluatorType { get; private set; }

    /// <summary>
    /// The evaluation score on a 0-5 scale.
    /// </summary>
    public decimal Score { get; private set; }

    /// <summary>
    /// Whether the output passed the evaluation threshold.
    /// </summary>
    public bool Pass { get; private set; }

    /// <summary>
    /// The rationale for the score.
    /// </summary>
    public string Feedback { get; private set; }

    // Navigation properties
    public virtual ModelRun ModelRun { get; private set; } = null!;

    // Private constructor for ORM/Serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Evaluation() : base() { }
#pragma warning restore CS8618

    /// <summary>
    /// Primary constructor for creating a new evaluation.
    /// </summary>
    public Evaluation(
        Guid id,
        Guid modelRunId,
        string evaluatorType,
        decimal score,
        string feedback) : base(id)
    {
        ModelRunId = modelRunId;
        EvaluatorType = evaluatorType ?? throw new ArgumentNullException(nameof(evaluatorType));
        Score = score;
        Feedback = feedback ?? throw new ArgumentNullException(nameof(feedback));
        
        // Determine if the evaluation passes (threshold of 3.5)
        Pass = score >= 3.5m;
    }

    /// <summary>
    /// Updates the evaluation score and feedback.
    /// </summary>
    public void UpdateEvaluation(decimal newScore, string newFeedback)
    {
        Score = newScore;
        Feedback = newFeedback ?? throw new ArgumentNullException(nameof(newFeedback));
        Pass = newScore >= 3.5m;
        UpdatedAt = DateTime.UtcNow;
    }
} 