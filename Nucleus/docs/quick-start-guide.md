# Nucleus Evaluation API - Quick Start Guide

This guide will help you get the Nucleus AI evaluation service up and running quickly.

## Prerequisites

- .NET 9.0 SDK
- PostgreSQL database
- OpenAI API key

## 1. Database Setup

### Create PostgreSQL Database
```sql
CREATE DATABASE nucleus_evaluation;
CREATE USER nucleus_user WITH PASSWORD 'your_secure_password';
GRANT ALL PRIVILEGES ON DATABASE nucleus_evaluation TO nucleus_user;
```

### Configure Connection String
Add to `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=nucleus_evaluation;Username=nucleus_user;Password=your_secure_password"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information",
      "Nucleus": "Debug"
    }
  }
}
```

## 2. Environment Variables

Create a `.env` file in the API project root:
```bash
# OpenAI Configuration
OPENAI_API_KEY=your_openai_api_key_here
OPENAI_MODEL=gpt-4
OPENAI_MAX_TOKENS=1000
OPENAI_TEMPERATURE=0.1

# Database (optional - can use appsettings instead)
# DATABASE_URL=postgresql://nucleus_user:password@localhost:5432/nucleus_evaluation
```

## 3. Database Migration

Run the database migrations:
```bash
cd src/Nucleus.Presentation/Nucleus.Api
dotnet ef database update
```

## 4. Build and Run

```bash
# Build the solution
dotnet build Nucleus.sln

# Run the API
cd src/Nucleus.Presentation/Nucleus.Api
dotnet run
```

The API will be available at:
- **API**: https://localhost:7001
- **Swagger**: https://localhost:7001/swagger
- **Health Check**: https://localhost:7001/api/evaluate/health

## 5. Test the API

### Health Check
```bash
curl https://localhost:7001/api/evaluate/health
```

### Sample Evaluation Request
```bash
curl -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d '{
    "workflowId": "test-workflow-001",
    "workflowName": "Test Summarization",
    "platform": "n8n",
    "executionId": "exec-123",
    "sessionId": "session-456",
    "triggeredBy": "manual",
    "mode": "test",
    "nodeId": "openai-summarize",
    "task": "Summarize",
    "modelName": "gpt-4",
    "modelProvider": "openai",
    "promptVersion": "v1.0",
    "inputData": {
      "originalText": "This is a long document that needs to be summarized into a concise version.",
      "maxLength": 50
    },
    "outputData": {
      "summary": "Document needs summarization.",
      "wordCount": 4
    }
  }'
```

## 6. Integration with n8n

See the [n8n Integration Templates](./n8n-integration-templates.md) for ready-to-use JSON templates.

## 7. Confirming the API is Running

After following the previous steps, your API should be running at https://localhost:7001.

- Test the health endpoint:
  ```bash
  curl -k https://localhost:7001/api/evaluate/health
  ```
  You should see a JSON response with `"status": "Healthy"`.

- Test the evaluation endpoint:
  ```bash
  curl -X POST https://localhost:7001/api/evaluate -H "Content-Type: application/json" -d @tests/TestData/test-request.json -k
  ```
  You should receive a JSON response with a score and feedback.

**Note:** LLM response parsing is now robust and fully tested for all common formats. All test data is in `tests/TestData/` and all tests are in `Nucleus.Application.Tests`.

## 8. Running Tests

- To run all tests:
  ```bash
  dotnet test
  ```
- To run only the application tests:
  ```bash
  dotnet test tests/Nucleus.Application.Tests/
  ```

## Troubleshooting

### Common Issues

**1. Database Connection Error**
- Verify PostgreSQL is running
- Check connection string format
- Ensure database and user exist

**2. OpenAI API Error**
- Verify OPENAI_API_KEY is set correctly
- Check API key has sufficient credits
- Ensure model name is valid

**3. Migration Errors**
- Ensure database exists
- Check user permissions

### Logs

Enable detailed logging by setting log levels in `