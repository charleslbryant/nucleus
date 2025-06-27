using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Interfaces;

/// <summary>
/// Repository interface for Evaluation entities.
/// </summary>
public interface IEvaluationRepository
{
    /// <summary>
    /// Gets an evaluation by its ID.
    /// </summary>
    Task<Evaluation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets evaluations by model run ID.
    /// </summary>
    Task<IEnumerable<Evaluation>> GetByModelRunIdAsync(Guid modelRunId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets evaluations by evaluator type.
    /// </summary>
    Task<IEnumerable<Evaluation>> GetByEvaluatorTypeAsync(string evaluatorType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets evaluations that failed (score < 3.5).
    /// </summary>
    Task<IEnumerable<Evaluation>> GetFailedEvaluationsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets evaluations by score range.
    /// </summary>
    Task<IEnumerable<Evaluation>> GetByScoreRangeAsync(decimal minScore, decimal maxScore, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new evaluation.
    /// </summary>
    Task<Evaluation> AddAsync(Evaluation evaluation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing evaluation.
    /// </summary>
    Task<Evaluation> UpdateAsync(Evaluation evaluation, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets recent evaluations with optional filtering.
    /// </summary>
    Task<IEnumerable<Evaluation>> GetRecentAsync(
        int limit = 100,
        string? evaluatorType = null,
        bool? pass = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets evaluation statistics.
    /// </summary>
    Task<EvaluationStatistics> GetStatisticsAsync(
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Statistics for evaluations.
/// </summary>
public record EvaluationStatistics(
    int TotalEvaluations,
    int PassedEvaluations,
    int FailedEvaluations,
    decimal AverageScore,
    decimal MinScore,
    decimal MaxScore); 