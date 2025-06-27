# Nucleus Tests

This folder contains all test projects for the Nucleus solution.

## Test Projects

### Nucleus.Application.Tests
Tests for the Application layer, including:
- **Services**: EvaluationService, validation, business logic
- **Features**: CQRS commands and queries
- **Integration**: Service integration tests

## Test Data

The `TestData` folder contains JSON files used for testing:
- `test-request.json` - Basic test request with minimal data
- `test-request-realistic.json` - Realistic test request with actual content

## Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test tests/Nucleus.Application.Tests/

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test class
dotnet test --filter "FullyQualifiedName~EvaluationServiceTests"
```

## Test Organization

Tests follow the same structure as the source code:
- `Services/` - Service layer tests
- `Features/` - CQRS feature tests
- `TestData/` - Test data files

## Test Conventions

- Use xUnit as the testing framework
- Use Moq for mocking dependencies
- Use FluentAssertions for readable assertions
- Follow AAA pattern (Arrange, Act, Assert)
- Test both happy path and edge cases
- Use descriptive test names that explain the scenario 