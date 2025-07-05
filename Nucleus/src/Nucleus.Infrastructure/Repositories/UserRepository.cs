using Microsoft.EntityFrameworkCore;
using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Interfaces;
using Nucleus.Infrastructure.Data;

namespace Nucleus.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for User entity operations.
/// </summary>
public class UserRepository(NucleusDbContext context) : IUserRepository
{
    private readonly NucleusDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc/>
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <inheritdoc/>
    public async Task<User?> GetByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        return await _context.Users
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync(u => u.Email == email.Trim().ToLowerInvariant());
    }

    /// <inheritdoc/>
    public async Task<User?> GetByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return null;

        return await _context.Users
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync(u => u.Username == username.Trim());
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Tenant)
            .OrderBy(u => u.Username)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<User>> GetByRoleAsync(UserRole role)
    {
        return await _context.Users
            .Include(u => u.Tenant)
            .Where(u => u.Role == role)
            .OrderBy(u => u.Username)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<User>> GetActiveAsync()
    {
        return await _context.Users
            .Include(u => u.Tenant)
            .Where(u => u.IsActive)
            .OrderBy(u => u.Username)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task AddAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task UpdateAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return await _context.Users
            .AnyAsync(u => u.Email == email.Trim().ToLowerInvariant());
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;

        return await _context.Users
            .AnyAsync(u => u.Username == username.Trim());
    }
} 