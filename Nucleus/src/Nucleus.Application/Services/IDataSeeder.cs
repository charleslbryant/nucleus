namespace Nucleus.Application.Services;

/// <summary>
/// Interface for seeding initial data.
/// </summary>
public interface IDataSeeder
{
    /// <summary>
    /// Seeds initial data including test users.
    /// </summary>
    /// <returns>Task representing the async operation</returns>
    Task SeedAsync();
} 