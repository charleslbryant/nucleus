using System.Text.Json;
using Nucleus.Domain.Common;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Events;

namespace Nucleus.Domain.Entities;

/// <summary>
/// Represents evaluation criteria templates that can be versioned and reused.
/// </summary>
public class EvaluationCriteria : BaseEntity
{
    public string Name { get; private set; }
    public string Version { get; private set; }
    public TaskType TaskType { get; private set; }
    public JsonDocument CriteriaDefinition { get; private set; }
    public bool IsDefault { get; private set; }

    private EvaluationCriteria() { } // For EF Core

    public EvaluationCriteria(
        Guid id,
        string name,
        string version,
        TaskType taskType = TaskType.Unknown,
        JsonDocument? criteriaDefinition = null,
        bool isDefault = false) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Version = version ?? throw new ArgumentNullException(nameof(version));
        
        if (taskType == TaskType.Unknown)
        {
            throw new ArgumentException("Task type must be specified and cannot be Unknown.", nameof(taskType));
        }
        TaskType = taskType;
        
        CriteriaDefinition = criteriaDefinition ?? throw new ArgumentNullException(nameof(criteriaDefinition));
        IsDefault = isDefault;
    }

    public static EvaluationCriteria CreateDefault(TaskType taskType = TaskType.Unknown)
    {
        if (taskType == TaskType.Unknown)
        {
            throw new ArgumentException("Cannot create default criteria for Unknown task type.", nameof(taskType));
        }

        var defaultCriteria = new
        {
            description = "Default evaluation criteria for AI model outputs",
            scale = new
            {
                min = 1,
                max = 5,
                description = "Quality scale from 1 (Poor) to 5 (Excellent)"
            },
            levels = new[]
            {
                new { score = 1, label = "Poor", description = "Completely incorrect, irrelevant, or harmful" },
                new { score = 2, label = "Below Average", description = "Mostly incorrect or missing key elements" },
                new { score = 3, label = "Average", description = "Partially correct but with significant issues" },
                new { score = 4, label = "Good", description = "Mostly correct with minor issues" },
                new { score = 5, label = "Excellent", description = "Accurate, complete, and well-executed" }
            },
            factors = new[]
            {
                new { name = "Accuracy", description = "Does the output correctly address the input?" },
                new { name = "Completeness", description = "Does it cover all necessary aspects?" },
                new { name = "Relevance", description = "Is it appropriate for the given task?" },
                new { name = "Quality", description = "Is it well-structured and coherent?" }
            },
            passThreshold = 3.5m
        };

        var jsonDoc = JsonDocument.Parse(JsonSerializer.Serialize(defaultCriteria));
        
        return new EvaluationCriteria(
            Guid.NewGuid(),
            "default",
            "1.0",
            taskType,
            jsonDoc,
            true);
    }

    public void UpdateCriteria(JsonDocument newCriteriaDefinition)
    {
        CriteriaDefinition = newCriteriaDefinition ?? throw new ArgumentNullException(nameof(newCriteriaDefinition));
        AddDomainEvent(new EvaluationCriteriaUpdatedEvent(this));
    }

    public void SetAsDefault()
    {
        IsDefault = true;
        AddDomainEvent(new EvaluationCriteriaSetAsDefaultEvent(this));
    }

    public void RemoveDefault()
    {
        IsDefault = false;
        AddDomainEvent(new EvaluationCriteriaRemovedAsDefaultEvent(this));
    }

    /// <summary>
    /// Validates that the task type is properly specified.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown when task type is Unknown.</exception>
    public void ValidateTaskType()
    {
        if (TaskType == TaskType.Unknown)
        {
            throw new InvalidOperationException("Task type must be specified and cannot be Unknown.");
        }
    }
} 