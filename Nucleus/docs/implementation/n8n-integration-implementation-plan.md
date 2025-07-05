# n8n Integration Implementation Plan

## Overview
This document outlines the complete implementation plan for integrating n8n workflows with the Nucleus AI evaluation service for live testing.

## Current Status
- ✅ **Nucleus API**: Running on https://localhost:7001
- ✅ **Database**: PostgreSQL with all migrations applied
- ✅ **LLM Integration**: OpenAI GPT evaluation working
- ✅ **n8n Templates**: Updated for local API integration
- ⏳ **Live Testing**: Ready to begin

## Implementation Plan

### Phase 1: Environment Setup

#### 1.1 n8n Installation and Configuration
**Objective**: Set up n8n for local development and testing

**Steps**:
1. **Install n8n** (if not already installed)
   ```bash
   npm install n8n -g
   ```

2. **Start n8n locally**
   ```bash
   n8n start
   ```
   - Default URL: http://localhost:5678
   - Create admin account when prompted

3. **Configure Environment Variables**
   ```bash
   # In n8n environment settings
   NUCLEUS_API_URL=https://localhost:7001
   OPENAI_API_KEY=your-openai-api-key
   ```

#### 1.2 API Connectivity Test
**Objective**: Verify n8n can communicate with Nucleus API

**Steps**:
1. **Test Nucleus API Health**
   - URL: `GET https://localhost:7001/api/evaluate/health`
   - Expected: `{"status":"healthy"}`

2. **Test Basic Evaluation**
   - Use n8n HTTP Request node
   - URL: `POST https://localhost:7001/api/evaluate`
   - Use test payload from `tests/TestData/test-request.json`

### Phase 2: Workflow Implementation

#### 2.1 Import Outlook Email Workflow
**Objective**: Import and configure the complete email summarization workflow

**Steps**:
1. **Import Workflow**
   - File: `docs/outlook-email-summarization-workflow.json`
   - Import via n8n UI: Settings → Import from File

2. **Configure Credentials**
   - **Microsoft Outlook**: Configure OAuth2 connection
   - **OpenAI**: Add API key credential
   - **HTTP Request**: Verify Nucleus API URL

3. **Update Node References**
   - Verify all node names match the imported workflow
   - Check data mapping expressions

#### 2.2 Workflow Configuration

**Node-by-Node Setup**:

1. **📧 Microsoft Outlook Trigger**
   - **Resource**: Message
   - **Operation**: GetAll
   - **Folder**: INBOX
   - **Limit**: 1 (for testing)
   - **Additional Fields**: subject, body, from, toRecipients, receivedDateTime

2. **🤖 OpenAI Summarize Email**
   - **Model**: gpt-4
   - **Temperature**: 0.3
   - **Max Tokens**: 300
   - **System Prompt**: Email summarization assistant
   - **User Prompt**: Template for email summarization

3. **🔍 Nucleus Evaluation**
   - **URL**: `https://localhost:7001/api/evaluate`
   - **Method**: POST
   - **Headers**: Content-Type: application/json
   - **Body**: Complete evaluation request with workflow and model metadata

4. **⚖️ Quality Check (Score >= 3.5)**
   - **Condition**: `{{$json.score >= 3.5}}`
   - **True Path**: High-quality summary processing
   - **False Path**: Human review notification

5. **✅ Success Actions**
   - **Mark Email as Processed**: Add categories to Outlook email
   - **Send Summary to Team**: Email with AI summary and evaluation

6. **🚨 Review Actions**
   - **Notify Human Reviewer**: Email with low-quality summary for manual review

### Phase 3: Testing Scenarios

#### 3.1 Test Data Preparation
**Objective**: Create test scenarios for comprehensive validation

**Test Scenarios**:

1. **High-Quality Email Summary**
   ```json
   {
     "subject": "Q4 Sales Report - Excellent Results",
     "body": "Team, I'm pleased to share our Q4 sales results. We achieved 125% of our target with $2.3M in revenue. Key highlights: 1) New product line contributed 40% of sales, 2) Customer retention improved to 95%, 3) We expanded into 3 new markets. Next steps: 1) Plan Q1 strategy meeting, 2) Prepare investor presentation, 3) Review bonus allocations. Great work everyone!",
     "sender": "ceo@company.com",
     "expected_score": "4.0-5.0"
   }
   ```

2. **Low-Quality Email Summary**
   ```json
   {
     "subject": "Meeting",
     "body": "Hi, we need to meet. Let me know when you're free. Thanks.",
     "sender": "colleague@company.com",
     "expected_score": "1.0-2.5"
   }
   ```

3. **Complex Email with Action Items**
   ```json
   {
     "subject": "Project Alpha - Critical Issues and Next Steps",
     "body": "Team, we've encountered several critical issues with Project Alpha that require immediate attention. Issues identified: 1) Database performance degradation during peak hours, 2) Security vulnerability in authentication module, 3) Integration failures with third-party APIs. Immediate actions needed: 1) Schedule emergency meeting today at 3 PM, 2) Prepare incident report for stakeholders, 3) Contact vendor support for API issues, 4) Review deployment timeline. This is urgent - please respond within 2 hours.",
     "sender": "project-manager@company.com",
     "expected_score": "3.5-4.5"
   }
   ```

#### 3.2 Manual Testing Process
**Objective**: Validate workflow functionality with real data

**Testing Steps**:

1. **Start Nucleus API**
   ```bash
   cd Nucleus
   dotnet run --project src/Nucleus.Presentation/Nucleus.Api
   ```

2. **Start n8n**
   ```bash
   n8n start
   ```

3. **Activate Workflow**
   - Open n8n UI: http://localhost:5678
   - Navigate to workflows
   - Activate "Outlook Email Summarization with Nucleus Evaluation"

4. **Send Test Emails**
   - Use Outlook or email client to send test emails
   - Monitor workflow execution in n8n
   - Check Nucleus API logs for evaluation requests

5. **Verify Results**
   - Check evaluation scores in Nucleus database
   - Verify email notifications are sent
   - Confirm workflow branching based on scores

#### 3.3 Automated Testing
**Objective**: Create automated tests for workflow validation

**Test Cases**:

1. **API Integration Test**
   ```bash
   # Test Nucleus API directly
   curl -X POST https://localhost:7001/api/evaluate \
     -H "Content-Type: application/json" \
     -d @tests/TestData/test-request.json
   ```

2. **Workflow Execution Test**
   - Use n8n's built-in testing features
   - Test each node individually
   - Validate data flow between nodes

3. **Error Handling Test**
   - Test API timeout scenarios
   - Test invalid request formats
   - Test network failure recovery

### Phase 4: Monitoring and Validation

#### 4.1 Real-Time Monitoring
**Objective**: Monitor workflow execution and evaluation results

**Monitoring Points**:

1. **n8n Execution Logs**
   - Workflow execution status
   - Node success/failure rates
   - Execution time metrics

2. **Nucleus API Logs**
   - Evaluation request volume
   - Response times
   - Error rates

3. **Database Monitoring**
   - Evaluation score distribution
   - Pass/fail rates
   - Trend analysis

#### 4.2 Performance Validation
**Objective**: Ensure workflow meets performance requirements

**Metrics to Track**:

1. **Response Times**
   - OpenAI API: < 10 seconds
   - Nucleus API: < 5 seconds
   - Total workflow: < 30 seconds

2. **Success Rates**
   - Workflow completion: > 95%
   - API calls: > 98%
   - Evaluation accuracy: > 90%

3. **Error Handling**
   - Graceful failure recovery
   - Proper error notifications
   - Retry mechanisms

### Phase 5: Documentation and Handover

#### 5.1 User Documentation
**Objective**: Create comprehensive documentation for end users

**Documentation Deliverables**:

1. **Setup Guide**
   - n8n installation and configuration
   - Credential setup
   - Workflow import instructions

2. **Usage Guide**
   - How to trigger the workflow
   - Understanding evaluation scores
   - Troubleshooting common issues

3. **Maintenance Guide**
   - Monitoring and alerting
   - Performance optimization
   - Error resolution

#### 5.2 Technical Documentation
**Objective**: Document technical implementation details

**Technical Deliverables**:

1. **Architecture Diagram**
   - Workflow node relationships
   - Data flow patterns
   - Integration points

2. **API Documentation**
   - Request/response formats
   - Error codes and handling
   - Rate limiting considerations

3. **Configuration Guide**
   - Environment variables
   - Credential management
   - Security considerations

## Success Criteria

### Functional Requirements
- [ ] n8n workflow successfully imports and activates
- [ ] Outlook email trigger works correctly
- [ ] OpenAI summarization generates quality summaries
- [ ] Nucleus API evaluation returns accurate scores
- [ ] Workflow branching works based on evaluation scores
- [ ] Email notifications are sent appropriately
- [ ] Error handling works for all failure scenarios

### Performance Requirements
- [ ] Total workflow execution time < 30 seconds
- [ ] API response times within acceptable limits
- [ ] Success rate > 95% for all components
- [ ] Proper error recovery and notification

### Quality Requirements
- [ ] Evaluation scores correlate with human assessment
- [ ] High-quality summaries are properly routed
- [ ] Low-quality summaries trigger human review
- [ ] All test scenarios pass validation

## Risk Mitigation

### Technical Risks
1. **API Connectivity Issues**
   - **Risk**: Network failures between n8n and Nucleus
   - **Mitigation**: Implement retry logic and timeout handling

2. **OpenAI API Rate Limits**
   - **Risk**: Exceeding API rate limits
   - **Mitigation**: Implement rate limiting and queuing

3. **Data Mapping Errors**
   - **Risk**: Incorrect data flow between nodes
   - **Mitigation**: Comprehensive testing and validation

### Operational Risks
1. **Credential Management**
   - **Risk**: Exposed API keys or credentials
   - **Mitigation**: Secure credential storage and rotation

2. **Monitoring Gaps**
   - **Risk**: Undetected failures or performance issues
   - **Mitigation**: Comprehensive logging and alerting

## Timeline

### Week 1: Environment Setup
- Day 1-2: n8n installation and configuration
- Day 3-4: API connectivity testing
- Day 5: Initial workflow import and validation

### Week 2: Workflow Implementation
- Day 1-3: Complete workflow configuration
- Day 4-5: Testing and validation

### Week 3: Testing and Optimization
- Day 1-3: Comprehensive testing scenarios
- Day 4-5: Performance optimization and monitoring

### Week 4: Documentation and Handover
- Day 1-3: User and technical documentation
- Day 4-5: Final validation and handover

## Next Steps

1. **Immediate Actions**
   - Install and configure n8n
   - Test Nucleus API connectivity
   - Import and validate workflow

2. **Validation Milestones**
   - Complete environment setup
   - Successful workflow execution
   - All test scenarios passing

3. **Production Readiness**
   - Performance validation
   - Error handling verification
   - Documentation completion

This implementation plan provides a comprehensive roadmap for successfully integrating n8n workflows with the Nucleus AI evaluation service, ensuring robust testing and validation of the complete system. 