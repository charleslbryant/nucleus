using MediatR;

namespace Nucleus.Application.Features.GetEvaluationHistory;

/// <summary>
/// Query for retrieving evaluation history with filtering options.
/// </summary>
public class GetEvaluationHistoryQuery : IRequest<GetEvaluationHistoryResponse>
{
    /// <summary>
    /// Maximum number of results to return.
    /// </summary>
    public int Limit { get; set; } = 100;

    /// <summary>
    /// Filter by platform.
    /// </summary>
    public string? Platform { get; set; }

    /// <summary>
    /// Filter by workflow name.
    /// </summary>
    public string? WorkflowName { get; set; }

    /// <summary>
    /// Filter by task type.
    /// </summary>
    public string? Task { get; set; }

    /// <summary>
    /// Filter by model name.
    /// </summary>
    public string? ModelName { get; set; }

    /// <summary>
    /// Filter by evaluator type.
    /// </summary>
    public string? EvaluatorType { get; set; }

    /// <summary>
    /// Filter by pass/fail status.
    /// </summary>
    public bool? Pass { get; set; }

    /// <summary>
    /// Filter by minimum score.
    /// </summary>
    public decimal? MinScore { get; set; }

    /// <summary>
    /// Filter by maximum score.
    /// </summary>
    public decimal? MaxScore { get; set; }

    /// <summary>
    /// Filter by start date.
    /// </summary>
    public DateTime? FromDate { get; set; }

    /// <summary>
    /// Filter by end date.
    /// </summary>
    public DateTime? ToDate { get; set; }
} 