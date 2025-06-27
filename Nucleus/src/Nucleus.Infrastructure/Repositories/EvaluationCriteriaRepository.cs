using Microsoft.EntityFrameworkCore;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Interfaces;
using Nucleus.Infrastructure.Data;

namespace Nucleus.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for EvaluationCriteria entities.
/// </summary>
public class EvaluationCriteriaRepository : IEvaluationCriteriaRepository
{
    private readonly NucleusDbContext _context;

    public EvaluationCriteriaRepository(NucleusDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<EvaluationCriteria?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.EvaluationCriteria
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<EvaluationCriteria?> GetDefaultByTaskTypeAsync(TaskType taskType, CancellationToken cancellationToken = default)
    {
        return await _context.EvaluationCriteria
            .Where(c => c.TaskType == taskType && c.IsDefault)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<EvaluationCriteria?> GetByNameAndVersionAsync(string name, string version, CancellationToken cancellationToken = default)
    {
        return await _context.EvaluationCriteria
            .Where(c => c.Name == name && c.Version == version)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<EvaluationCriteria>> GetByTaskTypeAsync(TaskType taskType, CancellationToken cancellationToken = default)
    {
        return await _context.EvaluationCriteria
            .Where(c => c.TaskType == taskType)
            .OrderByDescending(c => c.IsDefault)
            .ThenBy(c => c.Name)
            .ThenBy(c => c.Version)
            .ToListAsync(cancellationToken);
    }

    public async Task<EvaluationCriteria> AddAsync(EvaluationCriteria criteria, CancellationToken cancellationToken = default)
    {
        _context.EvaluationCriteria.Add(criteria);
        await _context.SaveChangesAsync(cancellationToken);
        return criteria;
    }

    public async Task<EvaluationCriteria> UpdateAsync(EvaluationCriteria criteria, CancellationToken cancellationToken = default)
    {
        _context.EvaluationCriteria.Update(criteria);
        await _context.SaveChangesAsync(cancellationToken);
        return criteria;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var criteria = await _context.EvaluationCriteria
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        if (criteria == null)
            return false;

        _context.EvaluationCriteria.Remove(criteria);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
} 