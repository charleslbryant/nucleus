using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Domain.Interfaces;
using Nucleus.Infrastructure.Data;
using Nucleus.Infrastructure.Repositories;

namespace Nucleus.Infrastructure;

/// <summary>
/// Dependency injection configuration for the Infrastructure layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds Infrastructure services to the service collection.
    /// </summary>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<NucleusDbContext>(options =>
        {
            // Try to get connection string from separate DB_* environment variables first
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbUser = Environment.GetEnvironmentVariable("DB_USER");
            var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
            
            string? connectionString = null;
            
            // If all DB_* variables are present, construct the connection string
            if (!string.IsNullOrEmpty(dbHost) && !string.IsNullOrEmpty(dbName) && 
                !string.IsNullOrEmpty(dbUser) && !string.IsNullOrEmpty(dbPassword))
            {
                var port = !string.IsNullOrEmpty(dbPort) ? dbPort : "5432";
                connectionString = $"Host={dbHost};Port={port};Database={dbName};Username={dbUser};Password={dbPassword}";
            }
            else
            {
                // Fallback to the old DATABASE_CONNECTION_STRING or configuration
                connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") 
                    ?? configuration.GetConnectionString("DefaultConnection");
            }
                
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string not found in environment variables or configuration. Please set DB_HOST, DB_NAME, DB_USER, and DB_PASSWORD environment variables.");
            }
            
            options.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null);
            });
        });

        // Register repositories
        services.AddScoped<IWorkflowRunRepository, WorkflowRunRepository>();
        services.AddScoped<IModelRunRepository, ModelRunRepository>();
        services.AddScoped<IEvaluationRepository, EvaluationRepository>();
        services.AddScoped<IEvaluationCriteriaRepository, EvaluationCriteriaRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
} 