namespace Nucleus.Application.Features.GetEvaluationHistory;

/// <summary>
/// Response for the GetEvaluationHistory query.
/// </summary>
public class GetEvaluationHistoryResponse
{
    /// <summary>
    /// List of evaluation history items.
    /// </summary>
    public List<EvaluationHistoryItem> Evaluations { get; set; } = new();

    /// <summary>
    /// Total count of evaluations matching the filter.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Statistics for the filtered results.
    /// </summary>
    public EvaluationStatistics Statistics { get; set; } = new();
}

/// <summary>
/// Individual evaluation history item.
/// </summary>
public class EvaluationHistoryItem
{
    /// <summary>
    /// Evaluation ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Evaluation score.
    /// </summary>
    public decimal Score { get; set; }

    /// <summary>
    /// Whether the evaluation passed.
    /// </summary>
    public bool Pass { get; set; }

    /// <summary>
    /// Evaluation feedback.
    /// </summary>
    public string Feedback { get; set; } = string.Empty;

    /// <summary>
    /// Evaluator type.
    /// </summary>
    public string EvaluatorType { get; set; } = string.Empty;

    /// <summary>
    /// When the evaluation was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Model run information.
    /// </summary>
    public ModelRunInfo ModelRun { get; set; } = new();

    /// <summary>
    /// Workflow run information.
    /// </summary>
    public WorkflowRunInfo WorkflowRun { get; set; } = new();
}

/// <summary>
/// Model run information for evaluation history.
/// </summary>
public class ModelRunInfo
{
    /// <summary>
    /// Model run ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Task being performed.
    /// </summary>
    public string Task { get; set; } = string.Empty;

    /// <summary>
    /// Model name.
    /// </summary>
    public string ModelName { get; set; } = string.Empty;

    /// <summary>
    /// Model provider.
    /// </summary>
    public string ModelProvider { get; set; } = string.Empty;

    /// <summary>
    /// Node ID.
    /// </summary>
    public string NodeId { get; set; } = string.Empty;
}

/// <summary>
/// Workflow run information for evaluation history.
/// </summary>
public class WorkflowRunInfo
{
    /// <summary>
    /// Workflow run ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Platform name.
    /// </summary>
    public string Platform { get; set; } = string.Empty;

    /// <summary>
    /// Workflow name.
    /// </summary>
    public string WorkflowName { get; set; } = string.Empty;

    /// <summary>
    /// External workflow ID.
    /// </summary>
    public string ExternalWorkflowId { get; set; } = string.Empty;

    /// <summary>
    /// External execution ID.
    /// </summary>
    public string ExternalExecutionId { get; set; } = string.Empty;
}

/// <summary>
/// Evaluation statistics.
/// </summary>
public class EvaluationStatistics
{
    /// <summary>
    /// Total number of evaluations.
    /// </summary>
    public int TotalEvaluations { get; set; }

    /// <summary>
    /// Number of passed evaluations.
    /// </summary>
    public int PassedEvaluations { get; set; }

    /// <summary>
    /// Number of failed evaluations.
    /// </summary>
    public int FailedEvaluations { get; set; }

    /// <summary>
    /// Average score.
    /// </summary>
    public decimal AverageScore { get; set; }

    /// <summary>
    /// Minimum score.
    /// </summary>
    public decimal MinScore { get; set; }

    /// <summary>
    /// Maximum score.
    /// </summary>
    public decimal MaxScore { get; set; }
} 