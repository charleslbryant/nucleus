namespace Nucleus.Application.Services;

/// <summary>
/// Implementation of OpenAI configuration from environment variables.
/// </summary>
public class OpenAIConfiguration : IOpenAIConfiguration
{
    public string ApiKey => Environment.GetEnvironmentVariable("OPENAI_API_KEY") 
        ?? throw new InvalidOperationException("OPENAI_API_KEY environment variable is not set.");

    public string Model => Environment.GetEnvironmentVariable("OPENAI_MODEL") ?? "gpt-4";

    public int MaxTokens => int.TryParse(Environment.GetEnvironmentVariable("OPENAI_MAX_TOKENS"), out var maxTokens) 
        ? maxTokens : 1000;

    public decimal Temperature => decimal.TryParse(Environment.GetEnvironmentVariable("OPENAI_TEMPERATURE"), out var temperature) 
        ? temperature : 0.1m;
} 