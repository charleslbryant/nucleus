using Nucleus.Domain.Enums;
using System.Text.Json;

namespace Nucleus.Application.Services;

/// <summary>
/// Service for evaluating AI model outputs using LLM.
/// </summary>
public interface IEvaluationService
{
    /// <summary>
    /// Evaluates a model output using LLM-based evaluation.
    /// </summary>
    /// <param name="task">The task being performed (summarize, draft, classify, etc.).</param>
    /// <param name="inputData">The input data sent to the model.</param>
    /// <param name="outputData">The output data returned by the model.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Evaluation result with score and feedback.</returns>
    Task<EvaluationResult> EvaluateModelOutputAsync(
        TaskType task,
        JsonDocument inputData,
        JsonDocument outputData,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// Result of an evaluation.
/// </summary>
public record EvaluationResult(
    decimal Score,
    string Feedback); 