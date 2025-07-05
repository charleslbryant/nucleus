# n8n Integration Templates for Nucleus Evaluation API

This document provides ready-to-use JSON templates for integrating n8n workflows with the Nucleus AI evaluation service.

## Overview

The Nucleus API provides a single endpoint `/api/evaluate` that accepts workflow and model run data, performs LLM-based evaluation, and returns quality scores and feedback.

**Endpoint**: `POST /api/evaluate`  
**Base URL**: `https://your-nucleus-api.com` (replace with your deployment URL)

## HTTP Request Node Configuration

### Basic Configuration
- **Method**: POST
- **URL**: `{{$json.baseUrl}}/api/evaluate`
- **Headers**: 
  - `Content-Type: application/json`
  - `Authorization: Bearer {{$json.apiKey}}` (if authentication is enabled)

### Request Body Template

```json
{
  "workflowId": "{{$json.workflowId}}",
  "workflowName": "{{$json.workflowName}}",
  "platform": "n8n",
  "executionId": "{{$json.executionId}}",
  "sessionId": "{{$json.sessionId}}",
  "triggeredBy": "{{$json.triggeredBy}}",
  "mode": "{{$json.mode}}",
  "nodeId": "{{$json.nodeId}}",
  "task": "{{$json.taskType}}",
  "modelName": "{{$json.modelName}}",
  "modelProvider": "{{$json.modelProvider}}",
  "promptVersion": "{{$json.promptVersion}}",
  "inputData": {{$json.inputData}},
  "outputData": {{$json.outputData}}
}
```

## Task Type Templates

### 1. Text Summarization

**Use Case**: Evaluate the quality of AI-generated text summaries

```json
{
  "workflowId": "summarization-workflow-001",
  "workflowName": "Document Summarization",
  "platform": "n8n",
  "executionId": "{{$json.executionId}}",
  "sessionId": "{{$json.sessionId}}",
  "triggeredBy": "webhook",
  "mode": "production",
  "nodeId": "openai-summarize",
  "task": "Summarize",
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "originalText": "{{$json.originalText}}",
    "maxLength": 200,
    "style": "concise"
  },
  "outputData": {
    "summary": "{{$json.generatedSummary}}",
    "wordCount": "{{$json.wordCount}}",
    "processingTime": "{{$json.processingTime}}"
  }
}
```

### 2. Text Classification

**Use Case**: Evaluate the accuracy of AI text classification

```json
{
  "workflowId": "classification-workflow-001",
  "workflowName": "Email Classification",
  "platform": "n8n",
  "executionId": "{{$json.executionId}}",
  "sessionId": "{{$json.sessionId}}",
  "triggeredBy": "email-trigger",
  "mode": "production",
  "nodeId": "openai-classify",
  "task": "Classify",
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "text": "{{$json.emailContent}}",
    "categories": ["urgent", "important", "routine", "spam"],
    "confidenceThreshold": 0.8
  },
  "outputData": {
    "classification": "{{$json.classification}}",
    "confidence": "{{$json.confidence}}",
    "reasoning": "{{$json.reasoning}}"
  }
}
```

### 3. Content Generation

**Use Case**: Evaluate the quality of AI-generated content

```json
{
  "workflowId": "content-generation-001",
  "workflowName": "Blog Post Generation",
  "platform": "n8n",
  "executionId": "{{$json.executionId}}",
  "sessionId": "{{$json.sessionId}}",
  "triggeredBy": "schedule",
  "mode": "production",
  "nodeId": "openai-generate",
  "task": "Generate",
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "topic": "{{$json.topic}}",
    "targetLength": 800,
    "tone": "professional",
    "keywords": "{{$json.keywords}}"
  },
  "outputData": {
    "generatedContent": "{{$json.generatedContent}}",
    "title": "{{$json.title}}",
    "wordCount": "{{$json.wordCount}}",
    "seoScore": "{{$json.seoScore}}"
  }
}
```

### 4. Data Extraction

**Use Case**: Evaluate the accuracy of AI data extraction

```json
{
  "workflowId": "data-extraction-001",
  "workflowName": "Invoice Data Extraction",
  "platform": "n8n",
  "executionId": "{{$json.executionId}}",
  "sessionId": "{{$json.sessionId}}",
  "triggeredBy": "file-watcher",
  "mode": "production",
  "nodeId": "openai-extract",
  "task": "Extract",
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "documentType": "invoice",
    "extractionFields": ["invoiceNumber", "amount", "date", "vendor"],
    "documentContent": "{{$json.documentContent}}"
  },
  "outputData": {
    "extractedData": {
      "invoiceNumber": "{{$json.invoiceNumber}}",
      "amount": "{{$json.amount}}",
      "date": "{{$json.date}}",
      "vendor": "{{$json.vendor}}"
    },
    "confidence": "{{$json.confidence}}",
    "extractionTime": "{{$json.extractionTime}}"
  }
}
```

### 5. Translation

**Use Case**: Evaluate the quality of AI translations

```json
{
  "workflowId": "translation-001",
  "workflowName": "Document Translation",
  "platform": "n8n",
  "executionId": "{{$json.executionId}}",
  "sessionId": "{{$json.sessionId}}",
  "triggeredBy": "webhook",
  "mode": "production",
  "nodeId": "openai-translate",
  "task": "Translate",
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "sourceText": "{{$json.sourceText}}",
    "sourceLanguage": "{{$json.sourceLanguage}}",
    "targetLanguage": "{{$json.targetLanguage}}",
    "context": "{{$json.context}}"
  },
  "outputData": {
    "translatedText": "{{$json.translatedText}}",
    "detectedLanguage": "{{$json.detectedLanguage}}",
    "translationQuality": "{{$json.translationQuality}}"
  }
}
```

### 6. Outlook Email Summarization

**Use Case**: Evaluate the quality of AI-generated email summaries

```json
{
  "workflowId": "outlook-summarization-001",
  "workflowName": "Outlook Email Summarization",
  "platform": "n8n",
  "executionId": "{{$json.executionId}}",
  "sessionId": "{{$json.sessionId}}",
  "triggeredBy": "email-trigger",
  "mode": "production",
  "nodeId": "openai-summarize-email",
  "task": "Summarize",
  "modelName": "gpt-4",
  "modelProvider": "openai",
  "promptVersion": "v1.0",
  "inputData": {
    "emailSubject": "{{$json.emailSubject}}",
    "emailBody": "{{$json.emailBody}}",
    "sender": "{{$json.sender}}",
    "recipients": "{{$json.recipients}}",
    "emailDate": "{{$json.emailDate}}",
    "maxLength": 150,
    "includeActionItems": true,
    "tone": "professional"
  },
  "outputData": {
    "summary": "{{$json.generatedSummary}}",
    "actionItems": "{{$json.actionItems}}",
    "priority": "{{$json.priority}}",
    "wordCount": "{{$json.wordCount}}",
    "processingTime": "{{$json.processingTime}}"
  }
}
```

## Response Handling

### Expected Response Format

```json
{
  "score": 4.2,
  "pass": true,
  "feedback": "The generated content is well-structured and addresses the topic effectively. Minor improvements could be made to enhance clarity.",
  "workflowRunId": "uuid-here",
  "modelRunId": "uuid-here",
  "evaluationId": "uuid-here",
  "evaluatorType": "LLM",
  "evaluatedAt": "2024-01-15T10:30:00Z"
}
```

### n8n If Node Configuration

Use the evaluation score to make decisions in your workflow:

**Condition**: `{{$json.score >= 3.5}}`

**High Quality Path** (score >= 3.5):
- Continue with normal processing
- Log success metrics
- Send to production

**Low Quality Path** (score < 3.5):
- Trigger human review
- Send notification to team
- Retry with different parameters
- Log for analysis

## Complete n8n Workflow Example

### Workflow Structure
1. **Trigger Node** (Webhook/Schedule)
2. **AI Processing Node** (OpenAI, Anthropic, etc.)
3. **HTTP Request Node** (Nucleus Evaluation)
4. **If Node** (Score-based branching)
5. **Action Nodes** (Success/Failure handling)

### Node Configuration

**HTTP Request Node - Nucleus Evaluation**:
```json
{
  "url": "https://your-nucleus-api.com/api/evaluate",
  "method": "POST",
  "headers": {
    "Content-Type": "application/json"
  },
  "body": {
    "workflowId": "{{$json.workflowId}}",
    "workflowName": "{{$json.workflowName}}",
    "platform": "n8n",
    "executionId": "{{$json.executionId}}",
    "nodeId": "{{$json.nodeId}}",
    "task": "{{$json.taskType}}",
    "modelName": "{{$json.modelName}}",
    "modelProvider": "{{$json.modelProvider}}",
    "inputData": {{$json.inputData}},
    "outputData": {{$json.outputData}}
  }
}
```

**If Node - Quality Check**:
```json
{
  "conditions": {
    "options": {
      "caseSensitive": true,
      "leftValue": "",
      "typeValidation": "strict"
    },
    "conditions": [
      {
        "id": "condition1",
        "leftValue": "={{$json.score}}",
        "rightValue": 3.5,
        "operator": {
          "type": "number",
          "operation": "gte"
        }
      }
    ],
    "combinator": "and"
  }
}
```

## Complete Outlook Email Workflow Example

### Workflow Structure
1. **Microsoft Outlook Trigger** (New Email)
2. **OpenAI Node** (Summarize Email)
3. **HTTP Request Node** (Nucleus Evaluation)
4. **If Node** (Score-based branching)
5. **Action Nodes** (Success/Failure handling)

### Node Configuration

**Microsoft Outlook Trigger**:
```json
{
  "resource": "message",
  "operation": "getAll",
  "folderId": "INBOX",
  "returnAll": false,
  "limit": 1,
  "additionalFields": {
    "select": "subject,body,from,toRecipients,receivedDateTime"
  }
}
```

**OpenAI Node - Email Summarization**:
```json
{
  "resource": "chat",
  "operation": "completion",
  "model": "gpt-4",
  "messages": [
    {
      "role": "system",
      "content": "You are an email summarization assistant. Create concise, professional summaries that capture key points and action items. Focus on clarity and actionable insights."
    },
    {
      "role": "user",
      "content": "Please summarize this email:\n\nSubject: {{$json.subject}}\nFrom: {{$json.from.emailAddress.address}}\nDate: {{$json.receivedDateTime}}\n\nBody:\n{{$json.body.content}}\n\nProvide a summary in 150 words or less, and list any action items if present."
    }
  ],
  "options": {
    "temperature": 0.3,
    "maxTokens": 300
  }
}
```

**HTTP Request Node - Nucleus Evaluation**:
```json
{
  "url": "https://your-nucleus-api.com/api/evaluate",
  "method": "POST",
  "headers": {
    "Content-Type": "application/json"
  },
  "body": {
    "workflowId": "outlook-summarization-001",
    "workflowName": "Outlook Email Summarization",
    "platform": "n8n",
    "executionId": "{{$json.executionId}}",
    "sessionId": "{{$json.sessionId}}",
    "triggeredBy": "email-trigger",
    "mode": "production",
    "nodeId": "openai-summarize-email",
    "task": "Summarize",
    "modelName": "gpt-4",
    "modelProvider": "openai",
    "promptVersion": "v1.0",
    "inputData": {
      "emailSubject": "{{$json.subject}}",
      "emailBody": "{{$json.body.content}}",
      "sender": "{{$json.from.emailAddress.address}}",
      "recipients": "{{$json.toRecipients}}",
      "emailDate": "{{$json.receivedDateTime}}",
      "maxLength": 150,
      "includeActionItems": true,
      "tone": "professional"
    },
    "outputData": {
      "summary": "{{$json.choices[0].message.content}}",
      "wordCount": "{{$json.usage.totalTokens}}",
      "processingTime": "{{$json.processingTime}}"
    }
  }
}
```

**If Node - Quality Check**:
```json
{
  "conditions": {
    "options": {
      "caseSensitive": true,
      "leftValue": "",
      "typeValidation": "strict"
    },
    "conditions": [
      {
        "id": "condition1",
        "leftValue": "={{$json.score}}",
        "rightValue": 3.5,
        "operator": {
          "type": "number",
          "operation": "gte"
        }
      }
    ],
    "combinator": "and"
  }
}
```

**High Quality Path (score >= 3.5)**:
- Save summary to database/CRM
- Send summary to team via Slack/Teams
- Mark email as processed

**Low Quality Path (score < 3.5)**:
- Send notification to human reviewer
- Include original email and generated summary
- Request manual review and feedback

## Environment Variables

Set these in your n8n environment:

```bash
NUCLEUS_API_URL=https://your-nucleus-api.com
NUCLEUS_API_KEY=your-api-key-if-needed
```

## Error Handling

### Common Error Responses

**400 Bad Request**:
```json
{
  "status": 400,
  "title": "Validation Error",
  "detail": "The request data is invalid.",
  "instance": "/api/evaluate"
}
```

**500 Internal Server Error**:
```json
{
  "status": 500,
  "title": "Internal Server Error",
  "detail": "An error occurred while processing the evaluation request.",
  "instance": "/api/evaluate"
}
```

### n8n Error Handling

Add an **Error Trigger** node to handle API failures:

```json
{
  "conditions": {
    "options": {
      "caseSensitive": true,
      "leftValue": "",
      "typeValidation": "strict"
    },
    "conditions": [
      {
        "id": "condition1",
        "leftValue": "={{$json.statusCode}}",
        "rightValue": 200,
        "operator": {
          "type": "number",
          "operation": "ne"
        }
      }
    ],
    "combinator": "and"
  }
}
```

## Best Practices

1. **Always validate input data** before sending to Nucleus
2. **Use meaningful workflow and node IDs** for better tracking
3. **Include context in inputData** to improve evaluation accuracy
4. **Handle both success and failure cases** in your workflow
5. **Log evaluation results** for monitoring and improvement
6. **Set up alerts** for consistently low scores
7. **Use different evaluation criteria** for different task types

## Monitoring and Analytics

Track these metrics in your n8n workflows:

- **Average evaluation scores** by task type
- **Pass/fail rates** over time
- **Common feedback themes** from low-scoring outputs
- **Performance trends** by model and provider
- **Error rates** and failure patterns

## Support

For issues with the Nucleus API integration:
1. Check the API health endpoint: `GET /api/evaluate/health`
2. Verify your request format matches the templates
3. Ensure all required fields are provided
4. Check the Nucleus API logs for detailed error information 