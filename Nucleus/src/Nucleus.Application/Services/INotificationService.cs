using Nucleus.Domain.Entities;

namespace Nucleus.Application.Services;

/// <summary>
/// Interface for notification services that trigger human feedback.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Sends a notification for human review when evaluation score is below threshold.
    /// </summary>
    /// <param name="evaluation">The evaluation that triggered the notification.</param>
    /// <param name="workflowRun">The workflow run context.</param>
    /// <param name="modelRun">The model run that was evaluated.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if notification was sent successfully, false otherwise.</returns>
    Task<bool> SendHumanFeedbackNotificationAsync(
        Evaluation evaluation,
        WorkflowRun workflowRun,
        ModelRun modelRun,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if human feedback should be triggered based on evaluation score.
    /// </summary>
    /// <param name="score">The evaluation score.</param>
    /// <returns>True if human feedback should be triggered.</returns>
    bool ShouldTriggerHumanFeedback(decimal score);
} 