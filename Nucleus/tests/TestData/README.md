# Test Data Files

This directory contains test data files for testing the Nucleus AI Evaluation API.

## Test Files Overview

### 1. `test-request.json`
**Purpose**: Basic evaluation test with moderate quality output
- **Expected Score**: 3-4 range
- **Human Feedback**: Should NOT trigger (score above 3.5 threshold)
- **Use Case**: Standard workflow testing

**Quick Test:**
```bash
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @test-request.json
```

### 2. `test-low-score-request.json`
**Purpose**: Test human feedback system with deliberately poor output
- **Expected Score**: 1-2 range
- **Human Feedback**: SHOULD trigger (score below 3.5 threshold)
- **Use Case**: Testing notification system

**Quick Test:**
```bash
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @test-low-score-request.json
```

### 3. `test-request-realistic.json`
**Purpose**: More realistic workflow scenario
- **Expected Score**: Variable based on output quality
- **Human Feedback**: Depends on score
- **Use Case**: Real-world workflow simulation

**Quick Test:**
```bash
curl -k -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @test-request-realistic.json
```

## Test Scenarios

### Scenario 1: Normal Quality Output
- **File**: `test-request.json`
- **Input**: Simple document for summarization
- **Output**: Basic summary
- **Expected**: Pass evaluation (score ≥ 3.5)

### Scenario 2: Poor Quality Output (Human Feedback Test)
- **File**: `test-low-score-request.json`
- **Input**: Complex AI/ML document
- **Output**: "AI stuff." (deliberately poor)
- **Expected**: Fail evaluation (score < 3.5), trigger human feedback

### Scenario 3: Realistic Workflow
- **File**: `test-request-realistic.json`
- **Input**: Real-world document
- **Output**: Variable quality
- **Expected**: Variable results based on output quality

## File Structure

All test files follow this structure:

```json
{
  "workflowId": "string",
  "workflowName": "string",
  "platform": "n8n|make|powerautomate",
  "executionId": "string",
  "sessionId": "string",
  "triggeredBy": "manual|webhook|cron",
  "mode": "test|production",
  "nodeId": "string",
  "task": 1,
  "modelName": "gpt-4|claude|etc",
  "modelProvider": "openai|anthropic|etc",
  "promptVersion": "string",
  "inputData": {
    // Task-specific input data
  },
  "outputData": {
    // AI model output to evaluate
  }
}
```

## Task Types

The `task` field corresponds to these enum values:
- `1`: Summarize
- `2`: Draft
- `3`: Classify
- `4`: Generate
- `5`: Analyze

## Creating Custom Tests

1. Copy an existing test file
2. Modify the input/output data
3. Update metadata (workflowId, executionId, etc.)
4. Test with curl command

**Example Custom Test:**
```json
{
  "workflowId": "my-custom-test",
  "workflowName": "Custom Evaluation Test",
  "platform": "n8n",
  "executionId": "exec-custom-001",
  "sessionId": "session-custom",
  "triggeredBy": "manual",
  "mode": "test",
  "nodeId": "my-ai-node",
  "task": 1,
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "text": "Your custom input here"
  },
  "outputData": {
    "result": "Your custom output here"
  }
}
```

## Batch Testing

Test all files at once:
```bash
for file in *.json; do
  echo "Testing $file..."
  curl -k -X POST https://localhost:7001/api/evaluate \
    -H "Content-Type: application/json" \
    -d @"$file"
  echo -e "\n---\n"
done
```

## Validation

Validate JSON syntax:
```bash
# Check if file is valid JSON
cat test-request.json | jq . > /dev/null && echo "Valid JSON" || echo "Invalid JSON"
```

## Related Documentation

- [API Testing Guide](../../docs/guides/api-testing-guide.md)
- [Memory Bank](../../memory-bank/)
- [Architecture Documentation](../../docs/architecture.md) 