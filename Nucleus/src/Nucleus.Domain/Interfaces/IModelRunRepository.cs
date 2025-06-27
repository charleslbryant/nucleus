using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;

namespace Nucleus.Domain.Interfaces;

/// <summary>
/// Repository interface for ModelRun entities.
/// </summary>
public interface IModelRunRepository
{
    /// <summary>
    /// Gets a model run by its ID.
    /// </summary>
    Task<ModelRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets model runs by workflow run ID.
    /// </summary>
    Task<IEnumerable<ModelRun>> GetByWorkflowRunIdAsync(Guid workflowRunId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets model runs by platform.
    /// </summary>
    Task<IEnumerable<ModelRun>> GetByPlatformAsync(string platform, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets model runs by task type.
    /// </summary>
    Task<IEnumerable<ModelRun>> GetByTaskAsync(TaskType task, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets model runs by model name.
    /// </summary>
    Task<IEnumerable<ModelRun>> GetByModelNameAsync(string modelName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new model run.
    /// </summary>
    Task<ModelRun> AddAsync(ModelRun modelRun, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing model run.
    /// </summary>
    Task<ModelRun> UpdateAsync(ModelRun modelRun, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets recent model runs with optional filtering.
    /// </summary>
    Task<IEnumerable<ModelRun>> GetRecentAsync(
        int limit = 100,
        string? platform = null,
        TaskType? task = null,
        string? modelName = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default);
} 