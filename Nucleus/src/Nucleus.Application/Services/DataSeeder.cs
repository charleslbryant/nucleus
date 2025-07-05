using Nucleus.Domain.Entities;
using Nucleus.Domain.Enums;
using Nucleus.Domain.Interfaces;

namespace Nucleus.Application.Services;

/// <summary>
/// Service for seeding initial data including test users.
/// </summary>
public class DataSeeder : IDataSeeder
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public DataSeeder(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
    }

    /// <inheritdoc/>
    public async Task SeedAsync()
    {
        await SeedTestUsersAsync();
    }

    private async Task SeedTestUsersAsync()
    {
        // Check if any users exist
        var existingUsers = await _userRepository.GetAllAsync();
        if (existingUsers.Any())
        {
            return; // Users already exist, skip seeding
        }

        // Create test users
        var testUsers = new[]
        {
            new
            {
                Email = "admin@nucleus.com",
                Username = "admin",
                Password = "admin123",
                Role = UserRole.Admin
            },
            new
            {
                Email = "reviewer@nucleus.com",
                Username = "reviewer",
                Password = "reviewer123",
                Role = UserRole.Reviewer
            },
            new
            {
                Email = "guest@nucleus.com",
                Username = "guest",
                Password = "guest123",
                Role = UserRole.Guest
            }
        };

        foreach (var testUser in testUsers)
        {
            // Check if user already exists
            if (await _userRepository.ExistsByEmailAsync(testUser.Email))
            {
                continue;
            }

            // Hash password and create user
            var passwordHash = _passwordHasher.HashPassword(testUser.Password);
            var user = new User(testUser.Email, testUser.Username, passwordHash, testUser.Role);
            
            await _userRepository.AddAsync(user);
        }
    }
} 