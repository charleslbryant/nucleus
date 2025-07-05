# PRD: Nucleus Evaluate API Endpoint

## Overview
A single API endpoint (`/api/evaluate`) for capturing, logging, and evaluating AI executions from external systems (n8n, Make, Power Automate, enterprise systems). There are many tools that can do this, but we are building to fine tune AI for AgenticOps.

## Jobs-to-be-Done
- AI Engineers need to evaluate AI outputs automatically
- Operators and operations teams want to monitor AI node quality
- Business users want reliable, high-quality AI solutions

## User Journey
1. User configures an Agentic workflow with digital, data, IoT, and AI nodes
2. Agentic workflow sends output to `/api/evaluate`
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