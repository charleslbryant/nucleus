using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Nucleus.Application.Features.EvaluateModelRun;
using Nucleus.Application.Features.GetEvaluationHistory;
using Nucleus.Application.Services;

namespace Nucleus.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Register application services
        services.AddScoped<IEvaluationService, EvaluationService>();
        services.AddSingleton<IOpenAIConfiguration, OpenAIConfiguration>();

        // Register validators explicitly
        services.AddTransient<IValidator<EvaluateModelRunCommand>, EvaluateModelRunCommandValidator>();

        return services;
    }
}