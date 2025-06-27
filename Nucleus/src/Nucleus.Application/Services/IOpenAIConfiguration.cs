namespace Nucleus.Application.Services;

/// <summary>
/// Configuration for OpenAI API integration.
/// </summary>
public interface IOpenAIConfiguration
{
    /// <summary>
    /// OpenAI API key.
    /// </summary>
    string ApiKey { get; }

    /// <summary>
    /// Model to use for evaluation (e.g., gpt-4, gpt-3.5-turbo).
    /// </summary>
    string Model { get; }

    /// <summary>
    /// Maximum tokens for the response.
    /// </summary>
    int MaxTokens { get; }

    /// <summary>
    /// Temperature for response generation (0.0 to 2.0).
    /// </summary>
    decimal Temperature { get; }
} 