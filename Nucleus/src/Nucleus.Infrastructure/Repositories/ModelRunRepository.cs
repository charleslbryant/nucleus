using Microsoft.EntityFrameworkCore;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Interfaces;
using Nucleus.Infrastructure.Data;

namespace Nucleus.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for ModelRun entities.
/// </summary>
public class ModelRunRepository : IModelRunRepository
{
    private readonly NucleusDbContext _context;

    public ModelRunRepository(NucleusDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<ModelRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ModelRuns
            .Include(m => m.WorkflowRun)
            .Include(m => m.Evaluations)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ModelRun>> GetByWorkflowRunIdAsync(Guid workflowRunId, CancellationToken cancellationToken = default)
    {
        return await _context.ModelRuns
            .Include(m => m.Evaluations)
            .Where(m => m.WorkflowRunId == workflowRunId)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ModelRun>> GetByPlatformAsync(string platform, CancellationToken cancellationToken = default)
    {
        return await _context.ModelRuns
            .Include(m => m.WorkflowRun)
            .Include(m => m.Evaluations)
            .Where(m => m.Platform == platform)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ModelRun>> GetByTaskAsync(TaskType task, CancellationToken cancellationToken = default)
    {
        return await _context.ModelRuns
            .Include(m => m.WorkflowRun)
            .Include(m => m.Evaluations)
            .Where(m => m.Task == task)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ModelRun>> GetByModelNameAsync(string modelName, CancellationToken cancellationToken = default)
    {
        return await _context.ModelRuns
            .Include(m => m.WorkflowRun)
            .Include(m => m.Evaluations)
            .Where(m => m.ModelName == modelName)
            .OrderByDescending(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<ModelRun> AddAsync(ModelRun modelRun, CancellationToken cancellationToken = default)
    {
        _context.ModelRuns.Add(modelRun);
        await _context.SaveChangesAsync(cancellationToken);
        return modelRun;
    }

    public async Task<ModelRun> UpdateAsync(ModelRun modelRun, CancellationToken cancellationToken = default)
    {
        _context.ModelRuns.Update(modelRun);
        await _context.SaveChangesAsync(cancellationToken);
        return modelRun;
    }

    public async Task<IEnumerable<ModelRun>> GetRecentAsync(
        int limit = 100,
        string? platform = null,
        TaskType? task = null,
        string? modelName = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.ModelRuns
            .Include(m => m.WorkflowRun)
            .Include(m => m.Evaluations)
            .AsQueryable();

        if (!string.IsNullOrEmpty(platform))
            query = query.Where(m => m.Platform == platform);

        if (task.HasValue)
            query = query.Where(m => m.Task == task.Value);

        if (!string.IsNullOrEmpty(modelName))
            query = query.Where(m => m.ModelName == modelName);

        if (fromDate.HasValue)
            query = query.Where(m => m.CreatedAt >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(m => m.CreatedAt <= toDate.Value);

        return await query
            .OrderByDescending(m => m.CreatedAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
} 