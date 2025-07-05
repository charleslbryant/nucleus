# Quick Start: n8n Integration with Nucleus

## Prerequisites
- ✅ Nucleus API running on https://localhost:7001
- ✅ PostgreSQL database with migrations applied
- ✅ OpenAI API key
- ⏳ n8n installation

## Step 1: Install n8n

```bash
# Install n8n globally
npm install n8n -g

# Start n8n
n8n start
```

**Access n8n UI**: http://localhost:5678
- Create admin account when prompted
- Note: First startup may take a few minutes

## Step 2: Test Nucleus API Connectivity

### 2.1 Health Check
```bash
curl -X GET https://localhost:7001/api/evaluate/health
```
**Expected Response**:
```json
{"status":"healthy"}
```

### 2.2 Test Evaluation Endpoint
```bash
curl -X POST https://localhost:7001/api/evaluate \
  -H "Content-Type: application/json" \
  -d @tests/TestData/test-request.json
```

**Expected Response**:
```json
{
  "score": 4.2,
  "pass": true,
  "feedback": "The generated content is well-structured...",
  "workflowRunId": "uuid-here",
  "modelRunId": "uuid-here",
  "evaluationId": "uuid-here",
  "evaluatorType": "LLM",
  "evaluatedAt": "2024-01-15T10:30:00Z"
}
```

## Step 3: Import and Test Workflow

### 3.1 Import Simple Test Workflow (Recommended)
1. Open n8n UI: http://localhost:5678
2. Go to **Settings** → **Import from File**
3. Select: `docs/simple-test-workflow.json`
4. Click **Import**

### 3.2 Configure OpenAI Credential
1. **OpenAI**:
   - Go to **Credentials** → **Add Credential**
   - Select **OpenAI**
   - Enter your OpenAI API key
   - Test connection

### 3.3 Alternative: Import Full Email Workflow
If you want to test the complete email workflow:
1. Import: `docs/outlook-email-summarization-workflow.json`
2. **Microsoft Graph** (instead of Outlook):
   - Go to **Credentials** → **Add Credential**
   - Select **Microsoft Graph**
   - Configure OAuth2 connection
   - Test connection

**Note**: Microsoft Outlook OAuth2 requires Azure App Registration setup. Microsoft Graph is more reliable for n8n integration.

### 3.4 Verify Workflow Configuration
1. Open the imported workflow
2. Check each node configuration:
   - **🔘 Manual Trigger**: Verify trigger settings
   - **🤖 OpenAI Summarize Email**: Verify model and prompts
   - **🔍 Nucleus Evaluation**: Verify URL and request format
   - **⚖️ Quality Check**: Verify condition logic
   - **✅ Success Response**: Verify response format
   - **🚨 Review Response**: Verify response format

## Step 4: Test the Workflow

### 4.1 Manual Testing
1. **Activate Workflow**:
   - Click **Activate** button
   - Workflow is ready for manual execution

2. **Execute Test**:
   - Click the **🔘 Manual Trigger** node
   - Click **Execute Node** button
   - Monitor workflow execution in n8n
   - Check Nucleus API logs

3. **Verify Results**:
   - Check evaluation score in n8n
   - Verify response format
   - Check Nucleus database for records

### 4.2 Test Scenarios

#### High-Quality Email Test
```json
{
  "subject": "Q4 Sales Report - Excellent Results",
  "body": "Team, I'm pleased to share our Q4 sales results. We achieved 125% of our target with $2.3M in revenue. Key highlights: 1) New product line contributed 40% of sales, 2) Customer retention improved to 95%, 3) We expanded into 3 new markets. Next steps: 1) Plan Q1 strategy meeting, 2) Prepare investor presentation, 3) Review bonus allocations. Great work everyone!"
}
```
**Expected**: Score >= 3.5, success path taken

#### Low-Quality Email Test
```json
{
  "subject": "Meeting",
  "body": "Hi, we need to meet. Let me know when you're free. Thanks."
}
```
**Expected**: Score < 3.5, review path taken

## Step 5: Monitor and Validate

### 5.1 Check Workflow Execution
- **n8n Execution Logs**: Monitor node success/failure
- **Nucleus API Logs**: Check evaluation requests
- **Database Records**: Verify data persistence

### 5.2 Performance Metrics
- **Total Execution Time**: Should be < 30 seconds
- **API Response Times**: Nucleus < 5s, OpenAI < 10s
- **Success Rate**: Should be > 95%

### 5.3 Error Handling
- **API Failures**: Check error recovery
- **Network Issues**: Verify timeout handling
- **Invalid Data**: Test validation responses

## Troubleshooting

### Common Issues

1. **n8n Won't Start**
   ```bash
   # Check if port 5678 is available
   netstat -an | grep 5678
   
   # Use different port
   n8n start --port 5679
   ```

2. **Nucleus API Connection Failed**
   ```bash
   # Check if Nucleus is running
   curl -X GET https://localhost:7001/api/evaluate/health
   
   # Check SSL certificate
   curl -k -X GET https://localhost:7001/api/evaluate/health
   ```

3. **OpenAI API Errors**
   - Verify API key is correct
   - Check API quota and rate limits
   - Test API key separately

4. **Microsoft OAuth2 Issues**
   - **Error**: "unauthorized_client: The client does not exist or is not enabled for consumers"
   - **Solution**: Use Microsoft Graph instead of Microsoft Outlook
   - **Alternative**: Configure Azure App Registration for Outlook
   - **Quick Fix**: Import the simple test workflow first

### Debug Mode
```bash
# Start n8n in debug mode
n8n start --debug

# Check detailed logs
tail -f ~/.n8n/logs/n8n.log
```

## Next Steps

1. **Complete Testing**: Run all test scenarios
2. **Performance Optimization**: Monitor and tune
3. **Production Setup**: Configure for production environment
4. **Documentation**: Create user guides and maintenance docs

## Support

- **n8n Documentation**: https://docs.n8n.io/
- **Nucleus API Docs**: Check `docs/` folder
- **Issues**: Check GitHub repository issues

---

**Status**: Ready for testing
**Last Updated**: 2024-01-15
**Version**: 1.0 