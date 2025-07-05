# Nucleus API Testing Guide

## Overview

This guide provides comprehensive instructions for testing the Nucleus AI Evaluation API using curl commands and the provided test data files.

## Prerequisites

- Nucleus API running on `https://localhost:7001`
- Test data files in `tests/TestData/`
- curl command-line tool
- Git Bash or similar shell environment

## API Endpoints

### Health Check
```bash
curl -k -s https://localhost:7001/api/evaluate/health
```

**Expected Response:**
```json
{
  "status": "Healthy",
  "service": "Nucleus Evaluation API",
  "version": "1.0.0",
  "timestamp": "2025-07-05T12:42:12.1234567Z"
}
```

### Evaluation Endpoint
```bash
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-request.json
```

## Test Data Files

### 1. Basic Test Request (`test-request.json`)
**Purpose**: Standard evaluation test with moderate quality output
**Expected Score**: 3-4 range
**Human Feedback**: Should NOT trigger (score above 3.5 threshold)

```bash
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-request.json
```

**Request Structure:**
```json
{
  "workflowId": "test-workflow-001",
  "workflowName": "Test Summarization",
  "platform": "n8n",
  "executionId": "exec-123",
  "sessionId": "session-456",
  "triggeredBy": "manual",
  "mode": "test",
  "nodeId": "openai-summarize",
  "task": 1,
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
}
```

### 2. Low Quality Test (`test-low-score-request.json`)
**Purpose**: Test human feedback system with deliberately poor output
**Expected Score**: 1-2 range
**Human Feedback**: SHOULD trigger (score below 3.5 threshold)

```bash
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-low-score-request.json
```

**Request Structure:**
```json
{
  "workflowId": "test-workflow-002",
  "workflowName": "Test Poor Quality Output",
  "platform": "n8n",
  "executionId": "exec-456",
  "sessionId": "session-789",
  "triggeredBy": "manual",
  "mode": "test",
  "nodeId": "openai-summarize",
  "task": 1,
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "originalText": "This is a comprehensive document about artificial intelligence and machine learning that contains detailed information about neural networks, deep learning algorithms, and their applications in various industries including healthcare, finance, and autonomous vehicles. The document discusses the evolution of AI from simple rule-based systems to complex neural networks and the challenges faced in implementing these technologies in real-world scenarios.",
    "maxLength": 100
  },
  "outputData": {
    "summary": "AI stuff.",
    "wordCount": 2
  }
}
```

### 3. Realistic Test Data (`test-request-realistic.json`)
**Purpose**: More realistic workflow scenario
**Expected Score**: Variable based on output quality

```bash
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-request-realistic.json
```

## Expected Responses

### Successful Evaluation Response
```json
{
  "score": 3.5,
  "pass": true,
  "feedback": "The output demonstrates good accuracy and relevance to the input. The response is well-structured and addresses the key requirements effectively.",
  "workflowRunId": "5c2555fc-96ba-40c1-b20a-97f1fd5b22f2",
  "modelRunId": "99d5910e-7862-4e0c-b324-e941da030175",
  "evaluationId": "8ef10435-3680-4e4d-9a00-3de68140cea8",
  "evaluatorType": "llm",
  "evaluatedAt": "2025-07-05T12:44:43.6196241Z"
}
```

### Low Score Response (Triggers Human Feedback)
```json
{
  "score": 1.0,
  "pass": false,
  "feedback": "The output is completely incorrect. There's no information provided in the input, thus the AI model didn't produce any summary. It doesn't meet the accuracy, completeness, relevance and quality criteria.",
  "workflowRunId": "5c2555fc-96ba-40c1-b20a-97f1fd5b22f2",
  "modelRunId": "d664fcd6-63d2-4015-ab38-c59ed018b295",
  "evaluationId": "9331474e-6b04-4ba5-aa6b-6b91a52f0c19",
  "evaluatorType": "llm",
  "evaluatedAt": "2025-07-05T12:44:43.6196241Z"
}
```

## Testing Scenarios

### 1. Health Check Test
```bash
# Test API health
curl -k -s https://localhost:7001/api/evaluate/health
```

### 2. Human Feedback Trigger Test
```bash
# Test with low quality output to trigger human feedback
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-low-score-request.json
```

### 3. Normal Quality Test
```bash
# Test with acceptable quality output
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-request.json
```

### 4. Realistic Workflow Test
```bash
# Test with realistic workflow data
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-request-realistic.json
```

## Response Field Explanations

| Field | Type | Description |
|-------|------|-------------|
| `score` | decimal | Evaluation score (1.0 - 5.0) |
| `pass` | boolean | Whether output passes quality threshold (≥3.5) |
| `feedback` | string | Detailed evaluation feedback |
| `workflowRunId` | UUID | Internal workflow run identifier |
| `modelRunId` | UUID | Internal model run identifier |
| `evaluationId` | UUID | Internal evaluation identifier |
| `evaluatorType` | string | Type of evaluator used ("llm") |
| `evaluatedAt` | DateTime | Timestamp of evaluation |

## Human Feedback System

### Threshold Configuration
- **Default Threshold**: 3.5
- **Configuration**: `appsettings.json` → `Notification.HumanFeedbackThreshold`
- **Trigger**: When score < threshold

### Notification Types
1. **Slack**: Webhook URL configuration required
2. **Email**: SMTP configuration required
3. **Logging**: Always logs notification attempts

### Testing Human Feedback
1. Use `test-low-score-request.json` (score ~1.0)
2. Check API logs for notification attempts
3. Verify threshold behavior with different scores

## Troubleshooting

### Common Issues

#### 1. API Not Running
```bash
# Check if API is running
curl -k -s https://localhost:7001/api/evaluate/health
# If connection refused, start API:
cd src/Nucleus.Presentation/Nucleus.Api
dotnet run
```

#### 2. SSL Certificate Issues
```bash
# Use -k flag to ignore SSL certificate
curl -k -X POST https://localhost:7001/api/evaluate ...
```

#### 3. File Not Found
```bash
# Ensure you're in the correct directory
cd /c/Users/charl/source/repos/workos/Nucleus
# Verify test files exist
ls -la tests/TestData/
```

#### 4. JSON Format Errors
```bash
# Validate JSON syntax
cat tests/TestData/test-request.json | jq .
```

### Debug Commands

#### Check API Status
```bash
curl -k -v https://localhost:7001/api/evaluate/health
```

#### Test with Verbose Output
```bash
curl -k -v -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-request.json
```

#### Check Response Headers
```bash
curl -k -I https://localhost:7001/api/evaluate/health
```

## Advanced Testing

### Custom Test Data
Create custom test files in `tests/TestData/`:

```json
{
  "workflowId": "custom-test",
  "workflowName": "Custom Test",
  "platform": "n8n",
  "executionId": "exec-custom",
  "sessionId": "session-custom",
  "triggeredBy": "manual",
  "mode": "test",
  "nodeId": "custom-node",
  "task": 1,
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "yourInput": "Custom input data"
  },
  "outputData": {
    "yourOutput": "Custom output data"
  }
}
```

### Batch Testing
```bash
# Test multiple scenarios
for file in tests/TestData/*.json; do
  echo "Testing $file..."
  curl -k -X POST https://localhost:7001/api/evaluate \
    -H "Content-Type: application/json" \
    -d @"$file"
  echo -e "\n---\n"
done
```

## Monitoring and Logs

### API Logs
Check the terminal where the API is running for:
- Evaluation processing logs
- Human feedback notification attempts
- Error messages and warnings

### Database Records
All evaluations are stored in the database with:
- Workflow run details
- Model run information
- Evaluation results and metadata

## Next Steps

1. **Configure Notifications**: Set up Slack webhook or email SMTP in `appsettings.json`
2. **Integration Testing**: Test with actual n8n workflows
3. **Performance Testing**: Load test with multiple concurrent requests
4. **Dashboard Development**: Build admin interface for viewing evaluations

---

*For more information, see the [Memory Bank](../memory-bank/) and [Architecture Documentation](../architecture.md).* 