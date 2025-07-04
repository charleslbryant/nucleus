# PRD: Nucleus Evaluate API Endpoint

## Overview
A single API endpoint (`/api/evaluate`) for capturing, logging, and evaluating AI node executions from external workflow platforms (n8n, Make, Power Automate).

## Jobs-to-be-Done
- Workflow developers need to evaluate AI outputs automatically
- Operations teams want to monitor AI node quality
- Business users want reliable, high-quality AI automation

## User Journey
1. User configures a workflow with an AI node
2. Workflow sends output to `/api/evaluate`
3. Nucleus logs the execution and evaluates output using LLM
4. If score is low, triggers human feedback
5. User reviews results in dashboard

## Requirements
- Accept POST requests with workflow execution data
- Log all executions with internal PKs and external refs
- Evaluate output using OpenAI GPT
- Trigger human feedback if score < threshold
- Return evaluation result in response
- Support multi-platform integration

## Success Criteria
- 100% of executions logged and evaluated
- Human feedback triggered only when needed
- Integration with n8n, Make, Power Automate
- API uptime > 99.9%

## Dependencies & Risks
- OpenAI API reliability
- Schema compatibility with multiple platforms

## Roadmap Priority
- Now

## Links
- [CRD for Evaluate Endpoint](../change-requests/CRD-nucleus-evaluate-endpoint.md) 