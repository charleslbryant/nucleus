# Issue #101: n8n Workflow Integration

## Overview
Enable seamless integration of Nucleus with n8n based Agentic workflows, allowing users to evaluate node outputs and branch logic based on evaluation scores.

## Jobs-to-be-Done
- **AI Engineers**: Want to automate quality checks in n8n
- **Operators and Operations Teams**: Want to reduce manual review

## User Journey
1. User adds HTTP node in n8n to call `/api/evaluate`
2. Receives evaluation score in workflow
3. Uses "If" node to branch logic (e.g., notify human if score < 3.5)

## Requirements
- [x] Provide JSON templates for n8n HTTP node
- [x] Document integration steps
- [x] Support branching based on evaluation score
- [x] Example workflows for common use cases

## Success Criteria
- [x] n8n users can integrate Nucleus in <10 minutes
- [x] Branching logic works reliably
- [x] Documentation is clear and complete

## Dependencies & Risks
- **Dependencies**: Nucleus Evaluate API Endpoint (#100)
- **Risks**: n8n HTTP node limitations, User error in workflow setup
- **Assumptions**: Users have basic n8n workflow knowledge

## Roadmap Priority
- **Priority**: High
- **Timeline**: Phase 2 (Platform Integration)
- **Phase**: Phase 2

## Implementation Status
- ✅ **COMPLETED**: JSON templates created
- ✅ **COMPLETED**: Integration documentation
- ✅ **COMPLETED**: Example workflows provided
- ✅ **COMPLETED**: Branching logic examples

## Related Links
- [PRD Document](../roadmap/product-requirements/PRD-n8n-integration.md)
- [CRD: n8n Integration](../roadmap/change-requests/CRD-n8n-integration.md)
- [Implementation: Human Feedback System](#121)
- [Implementation: Admin Dashboard](#122)

## Labels
- `prd`
- `requirement`
- `product`
- `completed`
- `phase-2`
- `integration` 