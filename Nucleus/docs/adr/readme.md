# Architecture Decision Records (ADRs)

## Purpose
Architecture Decision Records (ADRs) document significant architectural decisions made during the development of Nucleus. Each ADR captures the context, decision, consequences, and rationale for important technical choices that shape the system's architecture.

## Structure
- Each ADR is a standalone markdown file in this directory.
- ADRs are numbered sequentially (ADR-001, ADR-002, etc.).
- Each ADR follows a consistent template format.
- ADRs are immutable once accepted - new decisions create new ADRs.

## Usage
- Use the ADR template to document new architectural decisions.
- Reference ADRs in code comments, documentation, and discussions.
- Review ADRs when considering architectural changes.
- Update ADRs only to add new decisions, never to modify existing ones.

## ADR Status
- **Proposed**: Under consideration
- **Accepted**: Decision made and implemented
- **Deprecated**: Superseded by newer decision
- **Superseded**: Replaced by another ADR

## Related Docs
- [Architecture Overview](../architecture.md)
- [System Patterns](../../memory-bank/systemPatterns.md)
- [Technical Context](../../memory-bank/techContext.md)

## Current ADRs
- [ADR-001: Clean Architecture Pattern](ADR-001-clean-architecture.md)
- [ADR-002: ASP.NET Core Framework](ADR-002-aspnet-core.md)
- [ADR-003: PostgreSQL with JSONB](ADR-003-postgresql-jsonb.md)
- [ADR-004: Semantic Kernel for LLM Integration](ADR-004-semantic-kernel.md)
- [ADR-005: n8n Integration Strategy](ADR-005-n8n-integration.md)
- [ADR-006: Single API Endpoint Design](ADR-006-single-api-endpoint.md)
- [ADR-007: CQRS Pattern Implementation](ADR-007-cqrs-pattern.md)
- [ADR-008: Entity Framework Core](ADR-008-entity-framework.md) 