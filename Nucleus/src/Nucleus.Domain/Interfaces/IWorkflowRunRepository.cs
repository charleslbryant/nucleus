using Nucleus.Domain.Entities;

namespace Nucleus.Domain.Interfaces;

/// <summary>
/// Repository interface for WorkflowRun entities.
/// </summary>
public interface IWorkflowRunRepository
{
    /// <summary>
    /// Gets a workflow run by its ID.
    /// </summary>
    Task<WorkflowRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a workflow run by external execution ID and platform.
    /// </summary>
    Task<WorkflowRun?> GetByExternalExecutionIdAsync(string externalExecutionId, string platform, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets workflow runs by platform.
    /// </summary>
    Task<IEnumerable<WorkflowRun>> GetByPlatformAsync(string platform, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets workflow runs by session ID.
    /// </summary>
    Task<IEnumerable<WorkflowRun>> GetBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new workflow run.
    /// </summary>
    Task<WorkflowRun> AddAsync(WorkflowRun workflowRun, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing workflow run.
    /// </summary>
    Task<WorkflowRun> UpdateAsync(WorkflowRun workflowRun, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets recent workflow runs with optional filtering.
    /// </summary>
    Task<IEnumerable<WorkflowRun>> GetRecentAsync(
        int limit = 100,
        string? platform = null,
        string? workflowName = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default);
} 