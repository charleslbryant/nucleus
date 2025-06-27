using Microsoft.Extensions.Logging;
using Moq;
using Nucleus.Application.Services;
using Nucleus.Domain.Enums;
using System.Text.Json;
using Xunit;

namespace Nucleus.Application.Tests.Services;

public class EvaluationServiceTests
{
    private readonly Mock<ILogger<EvaluationService>> _mockLogger;
    private readonly Mock<IOpenAIConfiguration> _mockOpenAIConfig;
    private readonly EvaluationService _evaluationService;

    public EvaluationServiceTests()
    {
        _mockLogger = new Mock<ILogger<EvaluationService>>();
        _mockOpenAIConfig = new Mock<IOpenAIConfiguration>();
        
        _mockOpenAIConfig.Setup(x => x.ApiKey).Returns("test-key");
        _mockOpenAIConfig.Setup(x => x.Model).Returns("gpt-4");
        _mockOpenAIConfig.Setup(x => x.MaxTokens).Returns(1000);
        _mockOpenAIConfig.Setup(x => x.Temperature).Returns(0.1m);

        _evaluationService = new EvaluationService(_mockLogger.Object, _mockOpenAIConfig.Object);
    }

    [Fact]
    public void ParseEvaluationResponse_WithPerfectJson_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"{""score"": 4.2, ""pass"": true, ""feedback"": ""Excellent summary that captures key points accurately.""}";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4.2m, result.Score);
        Assert.Equal("Excellent summary that captures key points accurately.", result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithNumericScore_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"{""score"": 3.8, ""pass"": true, ""feedback"": ""Good summary with minor issues.""}";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3.8m, result.Score);
        Assert.Equal("Good summary with minor issues.", result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithJsonWrappedInMarkdown_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"Here's my evaluation:

```json
{
  ""score"": 4.5,
  ""pass"": true,
  ""feedback"": ""Very good summary that addresses the input effectively.""
}
```";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4.5m, result.Score);
        Assert.Equal("Very good summary that addresses the input effectively.", result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithScoreInText_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"Based on my evaluation, I would rate this output a 4.2 out of 5. The summary is accurate and well-structured.";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4.2m, result.Score);
        Assert.Equal(response, result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithQualityScore_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"The quality score is 3.9. This is a good summary with room for improvement.";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3.9m, result.Score);
        Assert.Equal(response, result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithRatingFormat_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"Rating: 4.1 - The output demonstrates good accuracy and relevance.";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4.1m, result.Score);
        Assert.Equal(response, result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithGradeFormat_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"Grade: 3.7 - Average performance with some issues.";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3.7m, result.Score);
        Assert.Equal(response, result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithMalformedJson_ShouldFallbackToRegex()
    {
        // Arrange
        var response = @"{""score"": 4.0, ""pass"": true, ""feedback"": ""Good output"""; // Missing closing brace

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4.0m, result.Score);
        Assert.Equal(response, result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithNoScore_ShouldReturnDefault()
    {
        // Arrange
        var response = @"This output is generally good but needs improvement in several areas.";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3.0m, result.Score);
        Assert.Contains("Failed to parse evaluation response", result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithEmptyResponse_ShouldReturnDefault()
    {
        // Arrange
        var response = "";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3.0m, result.Score);
        Assert.Contains("Failed to parse evaluation response", result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithScoreOutOfRange_ShouldClampToValidRange()
    {
        // Arrange
        var response = @"{""score"": 6.5, ""pass"": true, ""feedback"": ""Too high score""}";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(5.0m, result.Score); // Should be clamped to max 5.0
        Assert.Equal("Too high score", result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithNegativeScore_ShouldClampToValidRange()
    {
        // Arrange
        var response = @"{""score"": -1.5, ""pass"": false, ""feedback"": ""Too low score""}";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1.0m, result.Score); // Should be clamped to min 1.0
        Assert.Equal("Too low score", result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithContextualNumber_ShouldParseCorrectly()
    {
        // Arrange
        var response = @"The evaluation shows a quality level of 4.3, which indicates good performance.";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4.3m, result.Score);
        Assert.Equal(response, result.Feedback);
    }

    [Fact]
    public void ParseEvaluationResponse_WithRealApiResponse_ShouldParseCorrectly()
    {
        // Arrange - This simulates the actual API response format we saw in the logs
        var response = @"```json
{
  ""score"": 1.0,
  ""pass"": false,
  ""feedback"": ""There is no actual content in either the input or the output, thus the model did not perform any task.""
}
```";

        // Act
        var result = ParseEvaluationResponse(response);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1.0m, result.Score);
        Assert.Equal("There is no actual content in either the input or the output, thus the model did not perform any task.", result.Feedback);
    }

    // Helper method to access the private ParseEvaluationResponse method
    private EvaluationResult ParseEvaluationResponse(string response)
    {
        var method = typeof(EvaluationService).GetMethod("ParseEvaluationResponse", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        return method?.Invoke(_evaluationService, new object[] { response }) as EvaluationResult 
            ?? throw new InvalidOperationException("Failed to invoke ParseEvaluationResponse method");
    }
} 