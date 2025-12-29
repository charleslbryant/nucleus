# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Nucleus is an AI workflow evaluation platform that integrates with external workflow platforms (n8n, Make, Power Automate) to evaluate AI model outputs. It uses Clean Architecture with .NET 9 backend and Vue.js 3 frontend.

## Build & Development Commands

All commands run from `/Nucleus` directory.

### Backend (.NET)

```bash
# Build solution
dotnet build Nucleus.sln

# Run API (development)
dotnet run --project src/Nucleus.Presentation/Nucleus.Api/Nucleus.Api.csproj

# Run tests
dotnet test tests/Nucleus.Application.Tests/Nucleus.Application.Tests.csproj

# Run single test
dotnet test --filter "FullyQualifiedName~TestClassName.TestMethodName"
```

### Frontend (Vue.js)

```bash
# From Nucleus/src/Nucleus.Presentation/Nucleus.Ui/
npm install
npm run dev      # Development server with Vite
npm run build    # Production build
npm run lint     # ESLint
```

## Architecture

Clean Architecture with four layers (dependencies point inward):

```
Presentation → Application → Domain
Infrastructure → Application → Domain
```

**Nucleus.Domain**: Core entities (WorkflowRun, ModelRun, Evaluation), enums, events, interfaces. No external dependencies.

**Nucleus.Application**: Use cases, CQRS commands/queries, services (EvaluationService, AuthenticationService, JwtTokenService), DTOs, validators. Uses MediatR, FluentValidation, AutoMapper.

**Nucleus.Infrastructure**: PostgreSQL with EF Core, OpenAI API integration, external service implementations.

**Nucleus.Presentation**: API controllers (AuthController, EvaluateController, EvaluationsController) and Vue.js admin dashboard.

## Technology Stack

- **Backend**: ASP.NET Core (.NET 9), Entity Framework Core, PostgreSQL, MediatR, FluentValidation, AutoMapper
- **Auth**: JWT authentication
- **AI**: OpenAI GPT API integration via Semantic Kernel
- **Frontend**: Vue.js 3, Pinia, Tailwind CSS, Chart.js, Vite

## Key Patterns

- **Strategy Pattern**: Different evaluator types (LLM, human, rule-based)
- **Repository Pattern**: Data access abstraction
- **CQRS**: Commands/queries via MediatR for request handling
- **Feature-based organization**: `Application/Features/[FeatureName]/` contains Command, Handler, Validator, Response

## Configuration

- Copy `.env.example` to `.env` for environment variables
- **Never commit connection strings to appsettings.json** - use `.env` file
- Database: PostgreSQL with JSONB for flexible payload storage

## Terminal Commands

- **Never use `&&`** in terminal commands due to shell compatibility issues
- Use separate commands or semicolons instead
- Always get current date/time from terminal using `date` command before writing dates

## Git Workflow

- **Never push directly to `main`** - all changes via feature branches and PRs
- Branch naming: `feature/issue-{number}-{description}`
- Every branch must have a corresponding GitHub Issue
- Squash commits before PR

## Documentation

- Memory bank files in `docs/` for project context (activeContext.md, progress.md, etc.)
- ADRs in `docs/adr/` for architectural decisions
- Personal task logs in `docs/tasks/personal/{username}/`
