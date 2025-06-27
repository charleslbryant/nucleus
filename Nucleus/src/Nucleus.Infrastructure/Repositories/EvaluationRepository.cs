using Microsoft.EntityFrameworkCore;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Interfaces;
using Nucleus.Infrastructure.Data;

namespace Nucleus.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Evaluation entities.
/// </summary>
public class EvaluationRepository : IEvaluationRepository
{
    private readonly NucleusDbContext _context;

    public EvaluationRepository(NucleusDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Evaluation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.ModelRun)
            .ThenInclude(m => m.WorkflowRun)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Evaluation>> GetByModelRunIdAsync(Guid modelRunId, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Where(e => e.ModelRunId == modelRunId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Evaluation>> GetByEvaluatorTypeAsync(string evaluatorType, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.ModelRun)
            .ThenInclude(m => m.WorkflowRun)
            .Where(e => e.EvaluatorType == evaluatorType)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Evaluation>> GetFailedEvaluationsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.ModelRun)
            .ThenInclude(m => m.WorkflowRun)
            .Where(e => !e.Pass)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Evaluation>> GetByScoreRangeAsync(decimal minScore, decimal maxScore, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.ModelRun)
            .ThenInclude(m => m.WorkflowRun)
            .Where(e => e.Score >= minScore && e.Score <= maxScore)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Evaluation> AddAsync(Evaluation evaluation, CancellationToken cancellationToken = default)
    {
        _context.Evaluations.Add(evaluation);
        await _context.SaveChangesAsync(cancellationToken);
        return evaluation;
    }

    public async Task<Evaluation> UpdateAsync(Evaluation evaluation, CancellationToken cancellationToken = default)
    {
        _context.Evaluations.Update(evaluation);
        await _context.SaveChangesAsync(cancellationToken);
        return evaluation;
    }

    public async Task<IEnumerable<Evaluation>> GetRecentAsync(
        int limit = 100,
        string? evaluatorType = null,
        bool? pass = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Evaluations
            .Include(e => e.ModelRun)
            .ThenInclude(m => m.WorkflowRun)
            .AsQueryable();

        if (!string.IsNullOrEmpty(evaluatorType))
            query = query.Where(e => e.EvaluatorType == evaluatorType);

        if (pass.HasValue)
            query = query.Where(e => e.Pass == pass.Value);

        if (fromDate.HasValue)
            query = query.Where(e => e.CreatedAt >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(e => e.CreatedAt <= toDate.Value);

        return await query
            .OrderByDescending(e => e.CreatedAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }

    public async Task<EvaluationStatistics> GetStatisticsAsync(
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Evaluations.AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(e => e.CreatedAt >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(e => e.CreatedAt <= toDate.Value);

        var totalEvaluations = await query.CountAsync(cancellationToken);
        var passedEvaluations = await query.CountAsync(e => e.Pass, cancellationToken);
        var failedEvaluations = totalEvaluations - passedEvaluations;

        var scores = await query.Select(e => e.Score).ToListAsync(cancellationToken);
        var averageScore = scores.Any() ? scores.Average() : 0m;
        var minScore = scores.Any() ? scores.Min() : 0m;
        var maxScore = scores.Any() ? scores.Max() : 0m;

        return new EvaluationStatistics(
            totalEvaluations,
            passedEvaluations,
            failedEvaluations,
            averageScore,
            minScore,
            maxScore);
    }
} 