# Architecture Decision Records (ADRs)

## Purpose
Architecture Decision Records (ADRs) document significant architectural decisions made during the development of a project. Each ADR captures the context, decision, consequences, and rationale for important technical choices that shape the system's architecture.

## Structure
- Each ADR is a standalone markdown file in this directory.
- ADRs are numbered sequentially with 4 digits (ADR-0001, ADR-0002, etc.).
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
- [ADR-0001: Clean Architecture Pattern](ADR-0001-clean-architecture.md)
- [ADR-0002: ASP.NET Core Framework](ADR-0002-aspnet-core.md)
- [ADR-0003: PostgreSQL with JSONB](ADR-0003-postgresql-jsonb.md)
- [ADR-0004: Semantic Kernel for LLM Integration](ADR-0004-semantic-kernel.md)
- [ADR-0005: n8n Integration Strategy](ADR-0005-n8n-integration.md)
- [ADR-0006: Single API Endpoint Design](ADR-0006-single-api-endpoint.md)
- [ADR-0007: CQRS Pattern Implementation](ADR-0007-cqrs-pattern.md)
- [ADR-0008: Entity Framework Core](ADR-0008-entity-framework.md) 