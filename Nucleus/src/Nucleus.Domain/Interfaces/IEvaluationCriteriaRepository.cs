using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;

namespace Nucleus.Domain.Interfaces;

/// <summary>
/// Repository interface for EvaluationCriteria entities.
/// </summary>
public interface IEvaluationCriteriaRepository
{
    /// <summary>
    /// Gets evaluation criteria by ID.
    /// </summary>
    /// <param name="id">The criteria ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The evaluation criteria or null if not found.</returns>
    Task<EvaluationCriteria?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the default evaluation criteria for a task type.
    /// </summary>
    /// <param name="taskType">The task type (e.g., summarize, classify).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The default criteria for the task type or null if not found.</returns>
    Task<EvaluationCriteria?> GetDefaultByTaskTypeAsync(TaskType taskType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets evaluation criteria by name and version.
    /// </summary>
    /// <param name="name">The criteria name.</param>
    /// <param name="version">The criteria version.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The evaluation criteria or null if not found.</returns>
    Task<EvaluationCriteria?> GetByNameAndVersionAsync(string name, string version, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all evaluation criteria for a task type.
    /// </summary>
    /// <param name="taskType">The task type.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of evaluation criteria for the task type.</returns>
    Task<IEnumerable<EvaluationCriteria>> GetByTaskTypeAsync(TaskType taskType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new evaluation criteria.
    /// </summary>
    /// <param name="criteria">The evaluation criteria to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The added evaluation criteria.</returns>
    Task<EvaluationCriteria> AddAsync(EvaluationCriteria criteria, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing evaluation criteria.
    /// </summary>
    /// <param name="criteria">The evaluation criteria to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated evaluation criteria.</returns>
    Task<EvaluationCriteria> UpdateAsync(EvaluationCriteria criteria, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an evaluation criteria.
    /// </summary>
    /// <param name="id">The criteria ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if deleted, false if not found.</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
} 