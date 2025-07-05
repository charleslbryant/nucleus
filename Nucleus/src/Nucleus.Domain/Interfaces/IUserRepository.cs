using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;

namespace Nucleus.Domain.Interfaces;

/// <summary>
/// Repository interface for User entity operations.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User if found, null otherwise</returns>
    Task<User?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Gets a user by their email address.
    /// </summary>
    /// <param name="email">User's email address</param>
    /// <returns>User if found, null otherwise</returns>
    Task<User?> GetByEmailAsync(string email);
    
    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    /// <param name="username">User's username</param>
    /// <returns>User if found, null otherwise</returns>
    Task<User?> GetByUsernameAsync(string username);
    
    /// <summary>
    /// Gets all users in the system.
    /// </summary>
    /// <returns>Collection of all users</returns>
    Task<IEnumerable<User>> GetAllAsync();
    
    /// <summary>
    /// Gets users by their role.
    /// </summary>
    /// <param name="role">User role to filter by</param>
    /// <returns>Collection of users with the specified role</returns>
    Task<IEnumerable<User>> GetByRoleAsync(UserRole role);
    
    /// <summary>
    /// Gets active users only.
    /// </summary>
    /// <returns>Collection of active users</returns>
    Task<IEnumerable<User>> GetActiveAsync();
    
    /// <summary>
    /// Adds a new user to the repository.
    /// </summary>
    /// <param name="user">User to add</param>
    /// <returns>Task representing the async operation</returns>
    Task AddAsync(User user);
    
    /// <summary>
    /// Updates an existing user in the repository.
    /// </summary>
    /// <param name="user">User to update</param>
    /// <returns>Task representing the async operation</returns>
    Task UpdateAsync(User user);
    
    /// <summary>
    /// Deletes a user from the repository.
    /// </summary>
    /// <param name="user">User to delete</param>
    /// <returns>Task representing the async operation</returns>
    Task DeleteAsync(User user);
    
    /// <summary>
    /// Checks if a user with the given email exists.
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <returns>True if user exists, false otherwise</returns>
    Task<bool> ExistsByEmailAsync(string email);
    
    /// <summary>
    /// Checks if a user with the given username exists.
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <returns>True if user exists, false otherwise</returns>
    Task<bool> ExistsByUsernameAsync(string username);
} 