namespace Nucleus.Api.DTOs;

/// <summary>
/// Response DTO for the /api/evaluate endpoint.
/// </summary>
public class EvaluateModelRunResponse
{
    /// <summary>
    /// The evaluation score (0-5 scale).
    /// </summary>
    public decimal Score { get; set; }

    /// <summary>
    /// Whether the output passed the evaluation threshold.
    /// </summary>
    public bool Pass { get; set; }

    /// <summary>
    /// The rationale for the score.
    /// </summary>
    public string Feedback { get; set; } = string.Empty;

    /// <summary>
    /// The workflow run ID.
    /// </summary>
    public Guid WorkflowRunId { get; set; }

    /// <summary>
    /// The model run ID.
    /// </summary>
    public Guid ModelRunId { get; set; }

    /// <summary>
    /// The evaluation ID.
    /// </summary>
    public Guid EvaluationId { get; set; }

    /// <summary>
    /// The evaluator type used (llm, human, rule).
    /// </summary>
    public string EvaluatorType { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when the evaluation was performed.
    /// </summary>
    public DateTime EvaluatedAt { get; set; }
} 