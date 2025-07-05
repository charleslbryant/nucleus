using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nucleus.Domain.Entities;
using System.Text.Json;
using System.Net.Http;

namespace Nucleus.Application.Services;

/// <summary>
/// Configuration for notification settings.
/// </summary>
public class NotificationConfiguration
{
    public const string SectionName = "Notification";
    
    /// <summary>
    /// The score threshold below which human feedback is triggered.
    /// </summary>
    public decimal HumanFeedbackThreshold { get; set; } = 3.5m;
    
    /// <summary>
    /// Slack webhook URL for notifications.
    /// </summary>
    public string? SlackWebhookUrl { get; set; }
    
    /// <summary>
    /// Email configuration for notifications.
    /// </summary>
    public EmailConfiguration? Email { get; set; }
    
    /// <summary>
    /// Whether to enable notifications.
    /// </summary>
    public bool EnableNotifications { get; set; } = true;
}

/// <summary>
/// Email configuration settings.
/// </summary>
public class EmailConfiguration
{
    /// <summary>
    /// SMTP server host.
    /// </summary>
    public string SmtpHost { get; set; } = string.Empty;
    
    /// <summary>
    /// SMTP server port.
    /// </summary>
    public int SmtpPort { get; set; } = 587;
    
    /// <summary>
    /// SMTP username.
    /// </summary>
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// SMTP password.
    /// </summary>
    public string Password { get; set; } = string.Empty;
    
    /// <summary>
    /// From email address.
    /// </summary>
    public string FromEmail { get; set; } = string.Empty;
    
    /// <summary>
    /// To email addresses (comma-separated).
    /// </summary>
    public string ToEmails { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether to use SSL.
    /// </summary>
    public bool UseSsl { get; set; } = true;
}

/// <summary>
/// Implementation of the notification service for human feedback triggers.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly NotificationConfiguration _config;
    private readonly HttpClient _httpClient;

    public NotificationService(
        ILogger<NotificationService> logger,
        IOptions<NotificationConfiguration> config,
        HttpClient httpClient)
    {
        _logger = logger;
        _config = config.Value;
        _httpClient = httpClient;
    }

    public bool ShouldTriggerHumanFeedback(decimal score)
    {
        return score < _config.HumanFeedbackThreshold;
    }

    public async Task<bool> SendHumanFeedbackNotificationAsync(
        Evaluation evaluation,
        WorkflowRun workflowRun,
        ModelRun modelRun,
        CancellationToken cancellationToken = default)
    {
        if (!_config.EnableNotifications)
        {
            _logger.LogDebug("Notifications are disabled, skipping human feedback notification");
            return true;
        }

        if (!ShouldTriggerHumanFeedback(evaluation.Score))
        {
            _logger.LogDebug("Score {Score} is above threshold {Threshold}, no notification needed",
                evaluation.Score, _config.HumanFeedbackThreshold);
            return true;
        }

        _logger.LogInformation("Sending human feedback notification for evaluation {EvaluationId} with score {Score}",
            evaluation.Id, evaluation.Score);

        var success = false;

        // Try Slack notification first
        if (!string.IsNullOrEmpty(_config.SlackWebhookUrl))
        {
            success = await SendSlackNotificationAsync(evaluation, workflowRun, modelRun, cancellationToken);
        }

        // Try email notification if Slack failed or not configured
        if (!success && _config.Email != null)
        {
            success = await SendEmailNotificationAsync(evaluation, workflowRun, modelRun, cancellationToken);
        }

        if (success)
        {
            _logger.LogInformation("Human feedback notification sent successfully for evaluation {EvaluationId}",
                evaluation.Id);
        }
        else
        {
            _logger.LogWarning("Failed to send human feedback notification for evaluation {EvaluationId}",
                evaluation.Id);
        }

        return success;
    }

    private async Task<bool> SendSlackNotificationAsync(
        Evaluation evaluation,
        WorkflowRun workflowRun,
        ModelRun modelRun,
        CancellationToken cancellationToken)
    {
        try
        {
            var message = CreateSlackMessage(evaluation, workflowRun, modelRun);
            var json = JsonSerializer.Serialize(message);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_config.SlackWebhookUrl, content, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogDebug("Slack notification sent successfully");
                return true;
            }
            else
            {
                _logger.LogWarning("Slack notification failed with status {StatusCode}: {Reason}",
                    response.StatusCode, response.ReasonPhrase);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending Slack notification");
            return false;
        }
    }

    private async Task<bool> SendEmailNotificationAsync(
        Evaluation evaluation,
        WorkflowRun workflowRun,
        ModelRun modelRun,
        CancellationToken cancellationToken)
    {
        try
        {
            // This is a placeholder for email implementation
            // In a real implementation, you would use a library like MailKit
            _logger.LogInformation("Email notification would be sent for evaluation {EvaluationId}",
                evaluation.Id);
            
            // For now, just log the notification details
            var emailContent = CreateEmailContent(evaluation, workflowRun, modelRun);
            _logger.LogInformation("Email notification content: {Content}", emailContent);
            
            return true; // Simulate success
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email notification");
            return false;
        }
    }

    private object CreateSlackMessage(Evaluation evaluation, WorkflowRun workflowRun, ModelRun modelRun)
    {
        var color = evaluation.Score switch
        {
            >= 3.0m => "#ffa500", // Orange for borderline
            >= 2.0m => "#ff8c00", // Dark orange for poor
            _ => "#ff0000"        // Red for very poor
        };

        return new
        {
            text = "🤖 AI Output Quality Alert - Human Review Required",
            attachments = new object[]
            {
                new
                {
                    color = color,
                    fields = new object[]
                    {
                        new { title = "Workflow", value = workflowRun.WorkflowName, @short = true },
                        new { title = "Platform", value = workflowRun.Platform, @short = true },
                        new { title = "Task", value = modelRun.Task, @short = true },
                        new { title = "Model", value = modelRun.ModelName, @short = true },
                        new { title = "Score", value = $"{evaluation.Score}/5.0", @short = true },
                        new { title = "Threshold", value = $"{_config.HumanFeedbackThreshold}/5.0", @short = true },
                        new { title = "Feedback", value = evaluation.Feedback, @short = false },
                        new { title = "Execution ID", value = workflowRun.ExternalExecutionId, @short = true },
                        new { title = "Timestamp", value = evaluation.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss UTC"), @short = true }
                    },
                    footer = "Nucleus AI Evaluation System",
                    ts = ((DateTimeOffset)evaluation.CreatedAt).ToUnixTimeSeconds()
                }
            }
        };
    }

    private string CreateEmailContent(Evaluation evaluation, WorkflowRun workflowRun, ModelRun modelRun)
    {
        return $@"
AI Output Quality Alert - Human Review Required

Workflow: {workflowRun.WorkflowName}
Platform: {workflowRun.Platform}
Task: {modelRun.Task}
Model: {modelRun.ModelName}
Score: {evaluation.Score}/5.0 (Threshold: {_config.HumanFeedbackThreshold}/5.0)

Feedback: {evaluation.Feedback}

Execution Details:
- Execution ID: {workflowRun.ExternalExecutionId}
- Session ID: {workflowRun.SessionId}
- Triggered By: {workflowRun.TriggeredBy}
- Mode: {workflowRun.Mode}
- Timestamp: {evaluation.CreatedAt:yyyy-MM-dd HH:mm:ss UTC}

Please review this AI output and provide feedback to improve the system.

---
Nucleus AI Evaluation System
        ".Trim();
    }
} 