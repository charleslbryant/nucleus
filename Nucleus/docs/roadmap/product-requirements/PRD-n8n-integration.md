# PRD: n8n Workflow Integration

## Overview
Enable seamless integration of Nucleus with n8n workflows, allowing users to evaluate AI node outputs and branch logic based on evaluation scores.

## Jobs-to-be-Done
- Workflow developers want to automate quality checks in n8n
- Operations teams want to reduce manual review

## User Journey
1. User adds HTTP node in n8n to call `/api/evaluate`
2. Receives evaluation score in workflow
3. Uses If node to branch logic (e.g., notify human if score < 3.5)

## Requirements
- Provide JSON templates for n8n HTTP node
- Document integration steps
- Support branching based on evaluation score
- Example workflows for common use cases

## Success Criteria
- n8n users can integrate Nucleus in <10 minutes
- Branching logic works reliably
- Documentation is clear and complete

## Dependencies & Risks
- n8n HTTP node limitations
- User error in workflow setup

## Roadmap Priority
- Now

## Links
- [CRD for n8n Integration](../change-requests/CRD-n8n-integration.md) 