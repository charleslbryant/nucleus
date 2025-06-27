using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Nucleus.Domain.Enums;
using System.Text.Json;

namespace Nucleus.Application.Services;

/// <summary>
/// Implementation of the evaluation service using LLM.
/// </summary>
public class EvaluationService : IEvaluationService
{
    private readonly ILogger<EvaluationService> _logger;
    private readonly Kernel _kernel;
    private readonly KernelFunction _evaluationFunction;

    public EvaluationService(ILogger<EvaluationService> logger, IOpenAIConfiguration openAIConfig)
    {
        _logger = logger;
        
        // Initialize Semantic Kernel
        var builder = Kernel.CreateBuilder();
        
        // Add OpenAI chat completion service with configuration
        builder.AddOpenAIChatCompletion(
            modelId: openAIConfig.Model,
            apiKey: openAIConfig.ApiKey);
        
        _kernel = builder.Build();
        
        // Load the evaluation prompt from file
        var promptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Prompts", "evaluation-prompt.txt");
        if (!File.Exists(promptPath))
        {
            // Fallback to embedded prompt if file doesn't exist
            var fallbackPrompt = GetFallbackPrompt();
            _evaluationFunction = _kernel.CreateFunctionFromPrompt(fallbackPrompt);
        }
        else
        {
            var prompt = File.ReadAllText(promptPath);
            _evaluationFunction = _kernel.CreateFunctionFromPrompt(prompt);
        }
    }

    public async Task<EvaluationResult> EvaluateModelOutputAsync(
        TaskType task,
        JsonDocument inputData,
        JsonDocument outputData,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Starting evaluation for task: {Task}", task);

            // Prepare arguments for the prompt
            var arguments = new KernelArguments
            {
                ["task"] = task.ToString(),
                ["model_name"] = "gpt-4", // Default model name
                ["platform"] = "nucleus", // Default platform
                ["input_data"] = inputData.RootElement.ToString(),
                ["output_data"] = outputData.RootElement.ToString()
            };

            // Execute the evaluation
            var result = await _evaluationFunction.InvokeAsync(_kernel, arguments, cancellationToken);
            var response = result.GetValue<string>();

            _logger.LogDebug("Raw evaluation response: {Response}", response);

            // Parse the JSON response
            if (string.IsNullOrEmpty(response))
            {
                _logger.LogWarning("Received empty response from LLM evaluation");
                return new EvaluationResult(
                    Score: 1.0m,
                    Feedback: "Evaluation failed: Empty response from LLM");
            }

            var evaluation = ParseEvaluationResponse(response);

            _logger.LogInformation("Evaluation completed. Score: {Score}", evaluation.Score);

            return evaluation;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during evaluation for task: {Task}", task);
            
            // Return a fallback evaluation
            return new EvaluationResult(
                Score: 1.0m,
                Feedback: $"Evaluation failed: {ex.Message}");
        }
    }

    private EvaluationResult ParseEvaluationResponse(string response)
    {
        try
        {
            _logger.LogDebug("Attempting to parse LLM response: {Response}", response);

            // Method 1: Try to extract and parse JSON from the response
            var jsonResult = TryParseJsonResponse(response);
            if (jsonResult != null)
            {
                return jsonResult;
            }

            // Method 2: Try to extract score using various regex patterns
            var regexResult = TryParseWithRegex(response);
            if (regexResult != null)
            {
                return regexResult;
            }

            // Method 3: Try to find any number that could be a score
            var numberResult = TryParseAnyNumber(response);
            if (numberResult != null)
            {
                return numberResult;
            }

            throw new InvalidOperationException("Could not parse evaluation response from any method");
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to parse evaluation response: {Response}", response);
            
            // Return a default evaluation
            return new EvaluationResult(
                Score: 3.0m,
                Feedback: $"Failed to parse evaluation response: {response}");
        }
    }

    private EvaluationResult? TryParseJsonResponse(string response)
    {
        try
        {
            // Try to extract JSON from the response (in case it's wrapped in markdown or text)
            var jsonStart = response.IndexOf('{');
            var jsonEnd = response.LastIndexOf('}');
            
            if (jsonStart >= 0 && jsonEnd > jsonStart)
            {
                var jsonContent = response.Substring(jsonStart, jsonEnd - jsonStart + 1);
                var jsonDoc = JsonDocument.Parse(jsonContent);
                var root = jsonDoc.RootElement;
                
                decimal? score = null;
                string? feedback = null;

                // Try to get score - handle both string and number formats
                if (root.TryGetProperty("score", out var scoreElement))
                {
                    switch (scoreElement.ValueKind)
                    {
                        case JsonValueKind.String:
                            if (decimal.TryParse(scoreElement.GetString(), out var stringScore))
                                score = stringScore;
                            break;
                        case JsonValueKind.Number:
                            if (scoreElement.TryGetDecimal(out var numberScore))
                                score = numberScore;
                            break;
                    }
                }

                // Try to get feedback
                if (root.TryGetProperty("feedback", out var feedbackElement))
                {
                    feedback = feedbackElement.GetString();
                }

                if (score.HasValue)
                {
                    return new EvaluationResult(
                        Score: Math.Max(1.0m, Math.Min(5.0m, score.Value)),
                        Feedback: feedback ?? "No feedback provided");
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "JSON parsing failed for response: {Response}", response);
            return null;
        }
    }

    private EvaluationResult? TryParseWithRegex(string response)
    {
        try
        {
            // Multiple regex patterns to catch different formats
            var patterns = new[]
            {
                @"score["":\s]*([0-9]+\.?[0-9]*)",  // "score": 4.2 or score: 4.2
                @"rating["":\s]*([0-9]+\.?[0-9]*)", // "rating": 4.2
                @"grade["":\s]*([0-9]+\.?[0-9]*)",  // "grade": 4.2
                @"evaluation["":\s]*([0-9]+\.?[0-9]*)", // "evaluation": 4.2
                @"([0-9]+\.?[0-9]*)\s*out\s*of\s*5", // 4.2 out of 5
                @"score\s*is\s*([0-9]+\.?[0-9]*)",  // score is 4.2
                @"rated\s*([0-9]+\.?[0-9]*)\s*out\s*of\s*5", // rated 4.2 out of 5
            };

            foreach (var pattern in patterns)
            {
                var match = System.Text.RegularExpressions.Regex.Match(
                    response, 
                    pattern, 
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                
                if (match.Success && decimal.TryParse(match.Groups[1].Value, out var extractedScore))
                {
                    // Validate the score is in reasonable range (1-5)
                    if (extractedScore >= 1.0m && extractedScore <= 5.0m)
                    {
                        return new EvaluationResult(
                            Score: extractedScore,
                            Feedback: response);
                    }
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "Regex parsing failed for response: {Response}", response);
            return null;
        }
    }

    private EvaluationResult? TryParseAnyNumber(string response)
    {
        try
        {
            // Find all numbers in the response
            var numberMatches = System.Text.RegularExpressions.Regex.Matches(
                response, 
                @"([0-9]+\.?[0-9]*)", 
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            foreach (System.Text.RegularExpressions.Match match in numberMatches)
            {
                if (decimal.TryParse(match.Groups[1].Value, out var number))
                {
                    // Check if this number could be a reasonable score (1-5)
                    if (number >= 1.0m && number <= 5.0m)
                    {
                        // Additional validation: check if it's mentioned near score-related words
                        var contextStart = Math.Max(0, match.Index - 50);
                        var contextEnd = Math.Min(response.Length, match.Index + 50);
                        var context = response.Substring(contextStart, contextEnd - contextStart).ToLower();
                        
                        var scoreKeywords = new[] { "score", "rating", "grade", "evaluation", "quality", "level" };
                        if (scoreKeywords.Any(keyword => context.Contains(keyword)))
                        {
                            return new EvaluationResult(
                                Score: number,
                                Feedback: response);
                        }
                    }
                }
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "Number parsing failed for response: {Response}", response);
            return null;
        }
    }

    private string GetFallbackPrompt()
    {
        return @"You are an expert AI evaluator. Your task is to evaluate the quality of AI model outputs based on the given input and task context.

## Task Context
- **Task Type**: {{$task}}
- **Model**: {{$model_name}}
- **Platform**: {{$platform}}

## Input Data
{{$input_data}}

## Output to Evaluate
{{$output_data}}

## Evaluation Criteria
Rate the output quality on a scale of 1-5 where:
- **1 (Poor)**: Completely incorrect, irrelevant, or harmful
- **2 (Below Average)**: Mostly incorrect or missing key elements
- **3 (Average)**: Partially correct but with significant issues
- **4 (Good)**: Mostly correct with minor issues
- **5 (Excellent)**: Accurate, complete, and well-executed

Consider these factors:
- **Accuracy**: Does the output correctly address the input?
- **Completeness**: Does it cover all necessary aspects?
- **Relevance**: Is it appropriate for the given task?
- **Quality**: Is it well-structured and coherent?

## IMPORTANT: Response Format Requirements
You MUST respond with ONLY a valid JSON object in the exact format below. Do not include any other text, explanations, or markdown formatting.

```json
{
  ""score"": 4.2,
  ""pass"": true,
  ""feedback"": ""The output demonstrates good accuracy and relevance to the input. The response is well-structured and addresses the key requirements effectively.""
}
```

## Response Rules:
1. The score must be a number between 1.0 and 5.0 (can include decimals)
2. The pass field must be true if score >= 3.5, false otherwise
3. The feedback must be a string explaining your evaluation
4. Do not include any text before or after the JSON
5. Do not use markdown code blocks or formatting
6. Ensure the JSON is valid and properly formatted

## Evaluation";
    }
} 