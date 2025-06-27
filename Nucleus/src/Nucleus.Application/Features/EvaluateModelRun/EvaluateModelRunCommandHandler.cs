using MediatR;
using Microsoft.Extensions.Logging;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Interfaces;
using Nucleus.Application.Services;

namespace Nucleus.Application.Features.EvaluateModelRun;

/// <summary>
/// Handler for the EvaluateModelRunCommand.
/// </summary>
public class EvaluateModelRunCommandHandler(
    IWorkflowRunRepository workflowRunRepository,
    IModelRunRepository modelRunRepository,
    IEvaluationRepository evaluationRepository,
    IEvaluationService evaluationService,
    ILogger<EvaluateModelRunCommandHandler> logger)
    : IRequestHandler<EvaluateModelRunCommand, EvaluateModelRunResponse>
{
    private readonly IWorkflowRunRepository _workflowRunRepository = workflowRunRepository;
    private readonly IModelRunRepository _modelRunRepository = modelRunRepository;
    private readonly IEvaluationRepository _evaluationRepository = evaluationRepository;
    private readonly IEvaluationService _evaluationService = evaluationService;
    private readonly ILogger<EvaluateModelRunCommandHandler> _logger = logger;

    /// <summary>
    /// Handles the EvaluateModelRunCommand.
    /// </summary>
    /// <param name="request">The command request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Evaluation results.</returns>
    public async Task<EvaluateModelRunResponse> Handle(
        EvaluateModelRunCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling EvaluateModelRunCommand for workflow {WorkflowName} on platform {Platform}.",
            request.WorkflowName, request.Platform);

        try
        {
            // Step 1: Get or create workflow run
            var workflowRun = await GetOrCreateWorkflowRunAsync(request, cancellationToken);

            // Step 2: Create model run
            var modelRun = await CreateModelRunAsync(request, workflowRun.Id, cancellationToken);

            // Step 3: Perform evaluation
            var evaluation = await PerformEvaluationAsync(request, modelRun.Id, cancellationToken);

            // Step 4: Update workflow run as successful
            workflowRun.CompleteSuccessfully(DateTime.UtcNow);
            await _workflowRunRepository.UpdateAsync(workflowRun, cancellationToken);

            _logger.LogInformation("Successfully evaluated model run {ModelRunId} with score {Score}.",
                modelRun.Id, evaluation.Score);

            return new EvaluateModelRunResponse
            {
                Score = evaluation.Score,
                Pass = evaluation.Pass,
                Feedback = evaluation.Feedback,
                WorkflowRunId = workflowRun.Id,
                ModelRunId = modelRun.Id,
                EvaluationId = evaluation.Id,
                EvaluatorType = evaluation.EvaluatorType
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling EvaluateModelRunCommand for workflow {WorkflowName}.",
                request.WorkflowName);
            throw;
        }
    }

    private async Task<WorkflowRun> GetOrCreateWorkflowRunAsync(
        EvaluateModelRunCommand request,
        CancellationToken cancellationToken)
    {
        // Try to find existing workflow run
        var existingWorkflowRun = await _workflowRunRepository.GetByExternalExecutionIdAsync(
            request.ExecutionId, request.Platform, cancellationToken);

        if (existingWorkflowRun != null)
        {
            _logger.LogInformation("Found existing workflow run {WorkflowRunId} for execution {ExecutionId}.",
                existingWorkflowRun.Id, request.ExecutionId);
            return existingWorkflowRun;
        }

        // Create new workflow run
        var workflowRun = new WorkflowRun(
            Guid.NewGuid(),
            request.Platform,
            request.WorkflowId,
            request.WorkflowName,
            request.ExecutionId,
            request.TriggeredBy,
            request.Mode,
            DateTime.UtcNow,
            request.SessionId);

        var createdWorkflowRun = await _workflowRunRepository.AddAsync(workflowRun, cancellationToken);
        
        _logger.LogInformation("Created new workflow run {WorkflowRunId} for execution {ExecutionId}.",
            createdWorkflowRun.Id, request.ExecutionId);

        return createdWorkflowRun;
    }

    private async Task<ModelRun> CreateModelRunAsync(
        EvaluateModelRunCommand request,
        Guid workflowRunId,
        CancellationToken cancellationToken)
    {
        var modelRun = new ModelRun(
            Guid.NewGuid(),
            workflowRunId,
            request.Platform,
            request.NodeId,
            request.Task,
            request.ModelName,
            request.ModelProvider,
            request.InputData,
            request.OutputData,
            request.PromptVersion);

        var createdModelRun = await _modelRunRepository.AddAsync(modelRun, cancellationToken);
        
        _logger.LogInformation("Created model run {ModelRunId} for task {Task} with model {ModelName}.",
            createdModelRun.Id, request.Task, request.ModelName);

        return createdModelRun;
    }

    private async Task<Evaluation> PerformEvaluationAsync(
        EvaluateModelRunCommand request,
        Guid modelRunId,
        CancellationToken cancellationToken)
    {
        // Use the evaluation service to perform LLM-based evaluation
        var evaluationResult = await _evaluationService.EvaluateModelOutputAsync(
            request.Task,
            request.InputData,
            request.OutputData,
            cancellationToken);

        var evaluation = new Evaluation(
            Guid.NewGuid(),
            modelRunId,
            "llm", // Using LLM evaluator
            evaluationResult.Score,
            evaluationResult.Feedback);

        var createdEvaluation = await _evaluationRepository.AddAsync(evaluation, cancellationToken);
        
        _logger.LogInformation("Created evaluation {EvaluationId} with score {Score} for model run {ModelRunId}.",
            createdEvaluation.Id, evaluationResult.Score, modelRunId);

        return createdEvaluation;
    }
} 