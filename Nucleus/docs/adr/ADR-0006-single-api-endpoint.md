# ADR-006: Single API Endpoint Design

## Status
**Accepted**

## Context
Nucleus needs to provide a simple, consistent interface for workflow platforms to evaluate AI outputs. The API design must be easy to understand, implement, and maintain while supporting the core evaluation use case.

Forces at play:
- Need for simple integration with multiple workflow platforms
- Requirement for consistent request/response format
- Support for different AI task types and models
- API versioning and evolution strategy
- Error handling and validation
- Performance and scalability requirements

## Decision
We will use a single API endpoint `/api/evaluate` for all evaluation requests.

Key aspects of this decision:
- **Single Endpoint**: `POST /api/evaluate` handles all evaluation requests
- **Unified Request Format**: Single JSON schema for all task types
- **Task Type Enumeration**: Use `task` field to distinguish between different AI tasks
- **Flexible Data Storage**: JSONB fields for task-specific input/output data
- **Consistent Response**: Standardized response format with score and feedback
- **Health Endpoint**: `GET /api/evaluate/health` for monitoring

## Consequences

### Positive
- **Simplicity**: Single endpoint reduces integration complexity
- **Consistency**: Unified request/response format across all task types
- **Maintainability**: Easier to maintain and evolve single endpoint
- **Documentation**: Simpler API documentation and examples
- **Testing**: Easier to test and validate single endpoint
- **Monitoring**: Centralized monitoring and logging
- **Caching**: Can implement caching at endpoint level

### Negative
- **Flexibility**: Less flexibility for task-specific optimizations
- **Validation Complexity**: Need to validate different task types in single endpoint
- **Performance**: Single endpoint may become bottleneck
- **Versioning**: API changes affect all task types
- **Error Handling**: Generic error responses may be less specific

### Neutral
- **Scalability**: Can scale horizontally with load balancers
- **Security**: Single point for authentication and authorization

## Rationale
Single endpoint design was chosen over alternatives like:
- **Multiple Endpoints**: `/api/evaluate/summarize`, `/api/evaluate/classify`, etc.
- **RESTful Resources**: `/api/evaluations` with different HTTP methods
- **GraphQL**: Overkill for simple request/response pattern
- **gRPC**: More complex, less accessible for workflow platforms

The decision prioritizes simplicity and consistency over flexibility. The single endpoint approach aligns with our goal of easy integration with workflow platforms while maintaining clean architecture principles.

## Related Decisions
- [ADR-005: n8n Integration Strategy](ADR-005-n8n-integration.md)
- [ADR-003: PostgreSQL with JSONB](ADR-003-postgresql-jsonb.md)

## References
- [API Design Best Practices](https://restfulapi.net/rest-api-design-tutorial-with-example/)
- [n8n Integration Templates](../n8n-integration-templates.md)
- [Quick Start Guide](../quick-start-guide.md) 