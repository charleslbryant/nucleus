using Microsoft.AspNetCore.Mvc;
using MediatR;
using Nucleus.Application.Features.GetEvaluationHistory;
using System.Linq;

namespace Nucleus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public EvaluationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all evaluations with optional filtering
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<EvaluationHistoryItem>>> GetEvaluations(
        [FromQuery] string? platform = null,
        [FromQuery] string? workflow = null,
        [FromQuery] string? node = null,
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null,
        [FromQuery] decimal? scoreMin = null,
        [FromQuery] decimal? scoreMax = null,
        [FromQuery] bool? passed = null)
    {
        var query = new GetEvaluationHistoryQuery
        {
            Platform = platform,
            WorkflowName = workflow,
            MinScore = scoreMin,
            MaxScore = scoreMax,
            Pass = passed,
            FromDate = dateFrom,
            ToDate = dateTo,
            Limit = 1000 // Get more results for dashboard
        };

        var result = await _mediator.Send(query);
        
        // Apply node filtering on the result since it's not in the query
        var filteredEvaluations = result.Evaluations.AsEnumerable();
        if (!string.IsNullOrEmpty(node))
        {
            filteredEvaluations = filteredEvaluations.Where(e => 
                e.ModelRun.NodeId.Contains(node, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(filteredEvaluations.ToList());
    }

    /// <summary>
    /// Get a specific evaluation by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<EvaluationHistoryItem>> GetEvaluation(Guid id)
    {
        var query = new GetEvaluationHistoryQuery
        {
            Limit = 1000
        };

        var result = await _mediator.Send(query);
        var evaluation = result.Evaluations.FirstOrDefault(e => e.Id == id);
        
        if (evaluation == null)
        {
            return NotFound($"Evaluation with ID {id} not found");
        }

        return Ok(evaluation);
    }

    /// <summary>
    /// Get evaluation statistics
    /// </summary>
    [HttpGet("stats")]
    public async Task<ActionResult<object>> GetStats()
    {
        var query = new GetEvaluationHistoryQuery
        {
            Limit = 1000
        };
        var result = await _mediator.Send(query);
        var evaluations = result.Evaluations;

        if (!evaluations.Any())
        {
            return Ok(new
            {
                totalCount = 0,
                averageScore = 0,
                passRate = 0,
                platforms = new string[0],
                scoreDistribution = new { }
            });
        }

        var totalCount = evaluations.Count;
        var averageScore = evaluations.Average(e => e.Score);
        var passRate = (decimal)evaluations.Count(e => e.Pass) / totalCount * 100;
        var platforms = evaluations.Select(e => e.WorkflowRun.Platform).Distinct().ToArray();
        
        var scoreDistribution = new Dictionary<int, int>();
        for (int i = 0; i <= 5; i++)
        {
            scoreDistribution[i] = evaluations.Count(e => Math.Floor(e.Score) == i);
        }

        return Ok(new
        {
            totalCount,
            averageScore = Math.Round(averageScore, 2),
            passRate = Math.Round(passRate, 1),
            platforms,
            scoreDistribution
        });
    }
} 