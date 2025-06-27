using Microsoft.EntityFrameworkCore;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Interfaces;
using Nucleus.Infrastructure.Data;

namespace Nucleus.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for WorkflowRun entities.
/// </summary>
public class WorkflowRunRepository : IWorkflowRunRepository
{
    private readonly NucleusDbContext _context;

    public WorkflowRunRepository(NucleusDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<WorkflowRun?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.WorkflowRuns
            .Include(w => w.ModelRuns)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }

    public async Task<WorkflowRun?> GetByExternalExecutionIdAsync(string externalExecutionId, string platform, CancellationToken cancellationToken = default)
    {
        return await _context.WorkflowRuns
            .Include(w => w.ModelRuns)
            .FirstOrDefaultAsync(w => w.ExternalExecutionId == externalExecutionId && w.Platform == platform, cancellationToken);
    }

    public async Task<IEnumerable<WorkflowRun>> GetByPlatformAsync(string platform, CancellationToken cancellationToken = default)
    {
        return await _context.WorkflowRuns
            .Include(w => w.ModelRuns)
            .Where(w => w.Platform == platform)
            .OrderByDescending(w => w.StartedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<WorkflowRun>> GetBySessionIdAsync(string sessionId, CancellationToken cancellationToken = default)
    {
        return await _context.WorkflowRuns
            .Include(w => w.ModelRuns)
            .Where(w => w.SessionId == sessionId)
            .OrderByDescending(w => w.StartedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<WorkflowRun> AddAsync(WorkflowRun workflowRun, CancellationToken cancellationToken = default)
    {
        _context.WorkflowRuns.Add(workflowRun);
        await _context.SaveChangesAsync(cancellationToken);
        return workflowRun;
    }

    public async Task<WorkflowRun> UpdateAsync(WorkflowRun workflowRun, CancellationToken cancellationToken = default)
    {
        _context.WorkflowRuns.Update(workflowRun);
        await _context.SaveChangesAsync(cancellationToken);
        return workflowRun;
    }

    public async Task<IEnumerable<WorkflowRun>> GetRecentAsync(
        int limit = 100,
        string? platform = null,
        string? workflowName = null,
        DateTime? fromDate = null,
        DateTime? toDate = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.WorkflowRuns
            .Include(w => w.ModelRuns)
            .AsQueryable();

        if (!string.IsNullOrEmpty(platform))
            query = query.Where(w => w.Platform == platform);

        if (!string.IsNullOrEmpty(workflowName))
            query = query.Where(w => w.WorkflowName.Contains(workflowName));

        if (fromDate.HasValue)
            query = query.Where(w => w.StartedAt >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(w => w.StartedAt <= toDate.Value);

        return await query
            .OrderByDescending(w => w.StartedAt)
            .Take(limit)
            .ToListAsync(cancellationToken);
    }
} 