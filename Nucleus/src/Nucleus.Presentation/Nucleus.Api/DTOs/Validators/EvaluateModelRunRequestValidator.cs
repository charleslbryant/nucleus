using FluentValidation;
using Nucleus.Api.DTOs;
using Nucleus.Domain.Enums;

namespace Nucleus.Api.DTOs.Validators;

/// <summary>
/// Validator for the EvaluateModelRunRequest DTO.
/// </summary>
public class EvaluateModelRunRequestValidator : AbstractValidator<EvaluateModelRunRequest>
{
    public EvaluateModelRunRequestValidator()
    {
        // Workflow metadata validation
        RuleFor(x => x.WorkflowId)
            .NotEmpty()
            .WithMessage("Workflow ID is required.");

        RuleFor(x => x.WorkflowName)
            .NotEmpty()
            .WithMessage("Workflow name is required.");

        RuleFor(x => x.Platform)
            .NotEmpty()
            .WithMessage("Platform is required.")
            .Must(BeValidPlatform)
            .WithMessage("Platform must be one of: n8n, Make, Power Automate, or other supported platform.");

        RuleFor(x => x.ExecutionId)
            .NotEmpty()
            .WithMessage("Execution ID is required.");

        RuleFor(x => x.TriggeredBy)
            .NotEmpty()
            .WithMessage("Triggered by is required.");

        RuleFor(x => x.Mode)
            .NotEmpty()
            .WithMessage("Mode is required.")
            .Must(BeValidMode)
            .WithMessage("Mode must be 'test' or 'production'.");

        // Model metadata validation
        RuleFor(x => x.NodeId)
            .NotEmpty()
            .WithMessage("Node ID is required.");

        RuleFor(x => x.Task)
            .NotEqual(TaskType.Unknown)
            .WithMessage("Task type must be specified and cannot be Unknown.");

        RuleFor(x => x.ModelName)
            .NotEmpty()
            .WithMessage("Model name is required.");

        RuleFor(x => x.ModelProvider)
            .NotEmpty()
            .WithMessage("Model provider is required.")
            .Must(BeValidModelProvider)
            .WithMessage("Model provider must be one of: openai, anthropic, or other supported provider.");

        // Data validation
        RuleFor(x => x.InputData)
            .NotNull()
            .WithMessage("Input data is required.");

        RuleFor(x => x.OutputData)
            .NotNull()
            .WithMessage("Output data is required.");
    }

    private static bool BeValidPlatform(string platform)
    {
        var validPlatforms = new[] { "n8n", "Make", "Power Automate", "zapier", "integromat" };
        return validPlatforms.Contains(platform, StringComparer.OrdinalIgnoreCase);
    }

    private static bool BeValidMode(string mode)
    {
        var validModes = new[] { "test", "production" };
        return validModes.Contains(mode, StringComparer.OrdinalIgnoreCase);
    }

    private static bool BeValidModelProvider(string provider)
    {
        var validProviders = new[] { "openai", "anthropic", "google", "azure", "aws" };
        return validProviders.Contains(provider, StringComparer.OrdinalIgnoreCase);
    }
} 