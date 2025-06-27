using Nucleus.Domain.Common;
using Nucleus.Domain.Enums;
using System.Text.Json;

namespace Nucleus.Domain.Entities;

/// <summary>
/// Represents an AI model execution within a workflow run.
/// </summary>
public class ModelRun : BaseEntity
{
    /// <summary>
    /// The workflow run this model execution belongs to.
    /// </summary>
    public Guid WorkflowRunId { get; private set; }

    /// <summary>
    /// The automation platform name.
    /// </summary>
    public string Platform { get; private set; }

    /// <summary>
    /// The external node ID or name from the platform.
    /// </summary>
    public string ExternalNodeId { get; private set; }

    /// <summary>
    /// The task being performed (summarize, draft, classify, etc.).
    /// </summary>
    public TaskType Task { get; private set; }

    /// <summary>
    /// The model name (gpt-4, claude, etc.).
    /// </summary>
    public string ModelName { get; private set; }

    /// <summary>
    /// The model provider (openai, anthropic, etc.).
    /// </summary>
    public string ModelProvider { get; private set; }

    /// <summary>
    /// Optional version of the prompt used.
    /// </summary>
    public string? PromptVersion { get; private set; }

    /// <summary>
    /// The input payload sent to the model (stored as JSONB).
    /// </summary>
    public JsonDocument InputData { get; private set; }

    /// <summary>
    /// The output returned by the model (stored as JSONB).
    /// </summary>
    public JsonDocument OutputData { get; private set; }

    // Navigation properties
    public virtual WorkflowRun WorkflowRun { get; private set; } = null!;
    public virtual ICollection<Evaluation> Evaluations { get; private set; } = new List<Evaluation>();

    // Private constructor for ORM/Serialization
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ModelRun() : base() { }
#pragma warning restore CS8618

    /// <summary>
    /// Primary constructor for creating a new model run.
    /// </summary>
    public ModelRun(
        Guid id,
        Guid workflowRunId,
        string platform,
        string externalNodeId,
        TaskType task = TaskType.Unknown,
        string modelName = "",
        string modelProvider = "",
        JsonDocument? inputData = null,
        JsonDocument? outputData = null,
        string? promptVersion = null) : base(id)
    {
        WorkflowRunId = workflowRunId;
        Platform = platform ?? throw new ArgumentNullException(nameof(platform));
        ExternalNodeId = externalNodeId ?? throw new ArgumentNullException(nameof(externalNodeId));
        
        // Fail fast validation for task type
        if (task == TaskType.Unknown)
        {
            throw new ArgumentException("Task type must be specified and cannot be Unknown.", nameof(task));
        }
        Task = task;
        
        ModelName = modelName ?? throw new ArgumentNullException(nameof(modelName));
        ModelProvider = modelProvider ?? throw new ArgumentNullException(nameof(modelProvider));
        InputData = inputData ?? throw new ArgumentNullException(nameof(inputData));
        OutputData = outputData ?? throw new ArgumentNullException(nameof(outputData));
        PromptVersion = promptVersion;
    }

    /// <summary>
    /// Adds an evaluation to this model run.
    /// </summary>
    public void AddEvaluation(Evaluation evaluation)
    {
        if (evaluation == null) throw new ArgumentNullException(nameof(evaluation));
        
        Evaluations.Add(evaluation);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates that the task type is properly specified.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when task type is Unknown.</exception>
    public void ValidateTaskType()
    {
        if (Task == TaskType.Unknown)
        {
            throw new InvalidOperationException("Task type must be specified and cannot be Unknown.");
        }
    }
} 