using Nucleus.Domain.Common;
using System.Text.Json;

namespace Nucleus.Domain.Entities;

/// <summary>
/// Represents a workflow execution from an external automation platform.
/// </summary>
public class WorkflowRun : BaseEntity
{
    /// <summary>
    /// The automation platform name (e.g., n8n, Make, Power Automate).
    /// </summary>
    public string Platform { get; private set; }

    /// <summary>
    /// The external workflow ID from the platform.
    /// </summary>
    public string ExternalWorkflowId { get; private set; }

    /// <summary>
    /// The display name of the workflow.
    /// </summary>
    public string WorkflowName { get; private set; }

    /// <summary>
    /// The external execution ID from the platform.
    /// </summary>
    public string ExternalExecutionId { get; private set; }

    /// <summary>
    /// Optional session correlation ID for grouping related executions.
    /// </summary>
    public string? SessionId { get; private set; }

    /// <summary>
    /// The source that triggered the workflow (webhook, cron, manual, etc.).
    /// </summary>
    public string TriggeredBy { get; private set; }

    /// <summary>
    /// The execution mode (test or production).
    /// </summary>
    public string Mode { get; private set; }

    /// <summary>
    /// When the workflow execution started.
    /// </summary>
    public DateTime StartedAt { get; private set; }

    /// <summary>
    /// When the workflow execution completed.
    /// </summary>
    public DateTime? CompletedAt { get; private set; }

    /// <summary>
    /// Whether the workflow execution succeeded.
    /// </summary>
    public bool Success { get; private set; }

    /// <summary>
    /// Error details if the workflow failed.
    /// </summary>
    public string? ErrorMessage { get; private set; }

    // Navigation properties
    public virtual ICollection<ModelRun> ModelRuns { get; private set; } = new List<ModelRun>();

    // Private constructor for ORM/Serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private WorkflowRun() : base() { }
#pragma warning restore CS8618

    /// <summary>
    /// Primary constructor for creating a new workflow run.
    /// </summary>
    public WorkflowRun(
        Guid id,
        string platform,
        string externalWorkflowId,
        string workflowName,
        string externalExecutionId,
        string triggeredBy,
        string mode,
        DateTime startedAt,
        string? sessionId = null) : base(id)
    {
        Platform = platform ?? throw new ArgumentNullException(nameof(platform));
        ExternalWorkflowId = externalWorkflowId ?? throw new ArgumentNullException(nameof(externalWorkflowId));
        WorkflowName = workflowName ?? throw new ArgumentNullException(nameof(workflowName));
        ExternalExecutionId = externalExecutionId ?? throw new ArgumentNullException(nameof(externalExecutionId));
        TriggeredBy = triggeredBy ?? throw new ArgumentNullException(nameof(triggeredBy));
        Mode = mode ?? throw new ArgumentNullException(nameof(mode));
        StartedAt = startedAt;
        SessionId = sessionId;
        Success = false; // Initially false until completed
    }

    /// <summary>
    /// Marks the workflow run as completed with success status.
    /// </summary>
    public void CompleteSuccessfully(DateTime completedAt)
    {
        CompletedAt = completedAt;
        Success = true;
        ErrorMessage = null;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Marks the workflow run as completed with failure status.
    /// </summary>
    public void CompleteWithError(DateTime completedAt, string errorMessage)
    {
        CompletedAt = completedAt;
        Success = false;
        ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Adds a model run to this workflow run.
    /// </summary>
    public void AddModelRun(ModelRun modelRun)
    {
        if (modelRun == null) throw new ArgumentNullException(nameof(modelRun));
        
        ModelRuns.Add(modelRun);
        UpdatedAt = DateTime.UtcNow;
    }
} 