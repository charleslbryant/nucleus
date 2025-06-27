using Nucleus.Domain.Enums;
using System.Text.Json;

namespace Nucleus.Api.DTOs;

/// <summary>
/// Request DTO for the /api/evaluate endpoint.
/// </summary>
public class EvaluateModelRunRequest
{
    /// <summary>
    /// Workflow metadata
    /// </summary>
    public string WorkflowId { get; set; } = string.Empty;
    public string WorkflowName { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string ExecutionId { get; set; } = string.Empty;
    public string? SessionId { get; set; }
    public string TriggeredBy { get; set; } = string.Empty;
    public string Mode { get; set; } = string.Empty;

    /// <summary>
    /// Model metadata
    /// </summary>
    public string NodeId { get; set; } = string.Empty;
    public TaskType Task { get; set; } = TaskType.Unknown;
    public string ModelName { get; set; } = string.Empty;
    public string ModelProvider { get; set; } = string.Empty;
    public string? PromptVersion { get; set; }

    /// <summary>
    /// Model input and output data
    /// </summary>
    public object InputData { get; set; } = null!;
    public object OutputData { get; set; } = null!;
} 