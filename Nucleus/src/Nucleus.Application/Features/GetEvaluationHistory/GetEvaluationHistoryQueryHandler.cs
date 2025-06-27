using MediatR;
using Microsoft.Extensions.Logging;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Interfaces;

namespace Nucleus.Application.Features.GetEvaluationHistory;

/// <summary>
/// Handler for the GetEvaluationHistoryQuery.
/// </summary>
public class GetEvaluationHistoryQueryHandler(
    IEvaluationRepository evaluationRepository,
    ILogger<GetEvaluationHistoryQueryHandler> logger)
    : IRequestHandler<GetEvaluationHistoryQuery, GetEvaluationHistoryResponse>
{
    private readonly IEvaluationRepository _evaluationRepository = evaluationRepository;
    private readonly ILogger<GetEvaluationHistoryQueryHandler> _logger = logger;

    /// <summary>
    /// Handles the GetEvaluationHistoryQuery.
    /// </summary>
    /// <param name="request">The query request.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Evaluation history with statistics.</returns>
    public async Task<GetEvaluationHistoryResponse> Handle(
        GetEvaluationHistoryQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetEvaluationHistoryQuery with limit {Limit} and filters: Platform={Platform}, Task={Task}, Pass={Pass}",
            request.Limit, request.Platform, request.Task, request.Pass);

        try
        {
            // Get evaluations with filtering
            var evaluations = await _evaluationRepository.GetRecentAsync(
                request.Limit,
                request.EvaluatorType,
                request.Pass,
                request.FromDate,
                request.ToDate,
                cancellationToken);

            // Get statistics
            var statistics = await _evaluationRepository.GetStatisticsAsync(
                request.FromDate,
                request.ToDate,
                cancellationToken);

            // Convert to response DTOs
            var evaluationItems = evaluations.Select(e => new EvaluationHistoryItem
            {
                Id = e.Id,
                Score = e.Score,
                Pass = e.Pass,
                Feedback = e.Feedback,
                EvaluatorType = e.EvaluatorType,
                CreatedAt = e.CreatedAt,
                ModelRun = new ModelRunInfo
                {
                    Id = e.ModelRun.Id,
                    Task = e.ModelRun.Task.ToString(),
                    ModelName = e.ModelRun.ModelName,
                    ModelProvider = e.ModelRun.ModelProvider,
                    NodeId = e.ModelRun.ExternalNodeId
                },
                WorkflowRun = new WorkflowRunInfo
                {
                    Id = e.ModelRun.WorkflowRun.Id,
                    Platform = e.ModelRun.WorkflowRun.Platform,
                    WorkflowName = e.ModelRun.WorkflowRun.WorkflowName,
                    ExternalWorkflowId = e.ModelRun.WorkflowRun.ExternalWorkflowId,
                    ExternalExecutionId = e.ModelRun.WorkflowRun.ExternalExecutionId
                }
            }).ToList();

            // Apply additional filters that aren't handled by the repository
            var filteredItems = evaluationItems.Where(e =>
                (string.IsNullOrEmpty(request.Platform) || e.WorkflowRun.Platform.Equals(request.Platform, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.WorkflowName) || e.WorkflowRun.WorkflowName.Contains(request.WorkflowName, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.Task) || e.ModelRun.Task.Equals(request.Task, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(request.ModelName) || e.ModelRun.ModelName.Equals(request.ModelName, StringComparison.OrdinalIgnoreCase)) &&
                (!request.MinScore.HasValue || e.Score >= request.MinScore.Value) &&
                (!request.MaxScore.HasValue || e.Score <= request.MaxScore.Value)
            ).ToList();

            var response = new GetEvaluationHistoryResponse
            {
                Evaluations = filteredItems,
                TotalCount = filteredItems.Count,
                Statistics = new EvaluationStatistics
                {
                    TotalEvaluations = statistics.TotalEvaluations,
                    PassedEvaluations = statistics.PassedEvaluations,
                    FailedEvaluations = statistics.FailedEvaluations,
                    AverageScore = statistics.AverageScore,
                    MinScore = statistics.MinScore,
                    MaxScore = statistics.MaxScore
                }
            };

            _logger.LogInformation("Retrieved {Count} evaluation history items", filteredItems.Count);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling GetEvaluationHistoryQuery");
            throw;
        }
    }
} 