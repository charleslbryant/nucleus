using MediatR;
using Microsoft.AspNetCore.Mvc;
using Nucleus.Api.DTOs;
using Nucleus.Application.Features.EvaluateModelRun;
using System.Text.Json;
using FluentValidation;

namespace Nucleus.Api.Controllers;

/// <summary>
/// Controller for model evaluation endpoints.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EvaluateController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EvaluateController> _logger;

    public EvaluateController(IMediator mediator, ILogger<EvaluateController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Evaluates an AI model run output.
    /// </summary>
    /// <param name="request">The evaluation request containing workflow and model data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Evaluation results including score, pass/fail status, and feedback.</returns>
    /// <response code="200">Evaluation completed successfully.</response>
    /// <response code="400">Invalid request data.</response>
    /// <response code="500">Internal server error during evaluation.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Nucleus.Api.DTOs.EvaluateModelRunResponse), 200)]
    [ProducesResponseType(typeof(ProblemDetails), 400)]
    [ProducesResponseType(typeof(ProblemDetails), 500)]
    public async Task<ActionResult<Nucleus.Api.DTOs.EvaluateModelRunResponse>> EvaluateModelRun(
        [FromBody] EvaluateModelRunRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received evaluation request for workflow {WorkflowName} on platform {Platform}",
            request.WorkflowName, request.Platform);

        try
        {
            // Convert object to JsonDocument for application layer
            var inputDataJson = JsonSerializer.Serialize(request.InputData);
            var outputDataJson = JsonSerializer.Serialize(request.OutputData);
            var inputData = JsonDocument.Parse(inputDataJson);
            var outputData = JsonDocument.Parse(outputDataJson);

            // Map request DTO to command
            var command = new EvaluateModelRunCommand
            {
                WorkflowId = request.WorkflowId,
                WorkflowName = request.WorkflowName,
                Platform = request.Platform,
                ExecutionId = request.ExecutionId,
                SessionId = request.SessionId,
                TriggeredBy = request.TriggeredBy,
                Mode = request.Mode,
                NodeId = request.NodeId,
                Task = request.Task,
                ModelName = request.ModelName,
                ModelProvider = request.ModelProvider,
                PromptVersion = request.PromptVersion,
                InputData = inputData,
                OutputData = outputData
            };

            // Send command through MediatR
            var result = await _mediator.Send(command, cancellationToken);

            // Map result to response DTO
            var response = new Nucleus.Api.DTOs.EvaluateModelRunResponse
            {
                Score = result.Score,
                Pass = result.Pass,
                Feedback = result.Feedback,
                WorkflowRunId = result.WorkflowRunId,
                ModelRunId = result.ModelRunId,
                EvaluationId = result.EvaluationId,
                EvaluatorType = result.EvaluatorType,
                EvaluatedAt = DateTime.UtcNow
            };

            _logger.LogInformation("Evaluation completed successfully. Score: {Score}, Pass: {Pass}",
                result.Score, result.Pass);

            return Ok(response);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Validation error in evaluation request: {Message}", ex.Message);
            
            var problemDetails = new ProblemDetails
            {
                Status = 400,
                Title = "Validation Error",
                Detail = "The request data is invalid.",
                Instance = HttpContext.Request.Path
            };

            return BadRequest(problemDetails);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing evaluation request for workflow {WorkflowName}",
                request.WorkflowName);

            var problemDetails = new ProblemDetails
            {
                Status = 500,
                Title = "Internal Server Error",
                Detail = "An error occurred while processing the evaluation request.",
                Instance = HttpContext.Request.Path
            };

            return StatusCode(500, problemDetails);
        }
    }

    /// <summary>
    /// Health check endpoint for the evaluation service.
    /// </summary>
    /// <returns>Service status information.</returns>
    [HttpGet("health")]
    [ProducesResponseType(typeof(object), 200)]
    public ActionResult<object> Health()
    {
        return Ok(new
        {
            Status = "Healthy",
            Service = "Nucleus Evaluation API",
            Version = "1.0.0",
            Timestamp = DateTime.UtcNow
        });
    }
} 