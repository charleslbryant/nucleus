# Nucleus Architecture

## Overview

Nucleus follows Clean Architecture principles with a focus on separation of concerns, dependency inversion, and maintainability. This document defines the architectural patterns, layer responsibilities, and organization rules for the project.

## Architecture Layers

### 1. Domain Layer (`Nucleus.Domain`)
**Purpose**: Core business logic, entities, and domain rules that are independent of external concerns.

**Contains**:
- **Entities**: Core business objects (WorkflowRun, ModelRun, Evaluation)
- **Value Objects**: Immutable objects representing domain concepts
- **Domain Services**: Business logic that doesn't belong to a single entity
- **Domain Events**: Events that occur within the domain
- **Interfaces**: Contracts for domain services (e.g., `IEvaluationService`)
- **Domain Exceptions**: Business-specific exceptions

**Rules**:
- No dependencies on other layers
- Pure business logic only
- No infrastructure concerns (databases, external APIs, etc.)
- No framework dependencies

**Example Structure**:
```
Domain/
├── Entities/
│   ├── WorkflowRun.cs
│   ├── ModelRun.cs
│   └── Evaluation.cs
├── Services/
│   └── IEvaluationService.cs
├── Events/
├── Exceptions/
└── ValueObjects/
```

### 2. Application Layer (`Nucleus.Application`)
**Purpose**: Orchestrates domain objects to implement use cases and business workflows.

**Contains**:
- **Commands/Queries**: CQRS pattern for handling requests
- **Handlers**: Implementation of command/query handlers
- **DTOs**: Data transfer objects for API contracts
- **Validators**: Input validation logic
- **Application Services**: Orchestration of domain services
- **Mappers**: Object-to-object mapping

**Rules**:
- Depends only on Domain layer
- Contains use case logic
- No direct infrastructure dependencies
- Can depend on external libraries (MediatR, FluentValidation, etc.)

**Example Structure**:
```
Application/
├── Features/
│   ├── EvaluateModelRun/
│   │   ├── EvaluateModelRunCommand.cs
│   │   ├── EvaluateModelRunHandler.cs
│   │   └── EvaluateModelRunValidator.cs
│   └── GetEvaluationHistory/
├── Services/
│   └── EvaluationService.cs
├── DTOs/
└── Mappers/
```

### 3. Infrastructure Layer (`Nucleus.Infrastructure`)
**Purpose**: Implements interfaces defined in Domain and Application layers, handles external concerns.

**Contains**:
- **Data Access**: Entity Framework, repositories, database context
- **External Services**: OpenAI API, email services, etc.
- **Configuration**: Environment-specific settings
- **Logging**: Logging implementations
- **Caching**: Cache implementations
- **File Storage**: File system operations

**Rules**:
- Implements interfaces from Domain/Application layers
- Contains all external dependencies
- Framework-specific implementations
- Configuration and environment concerns

**Example Structure**:
```
Infrastructure/
├── Data/
│   ├── Context/
│   ├── Repositories/
│   └── Migrations/
├── Services/
│   ├── OpenAI/
│   ├── Email/
│   └── FileStorage/
├── Configuration/
└── Logging/
```

### 4. Presentation Layer (`Nucleus.Presentation`)

The Presentation Layer handles all external interfaces and user interactions:

- **API Controllers**: REST API endpoints for external workflow platforms
- **DTOs**: Data Transfer Objects for API requests and responses
- **Validation**: Input validation using FluentValidation
- **Error Handling**: Global exception handling and error responses
- **Authentication**: JWT token validation (future)
- **UI Components**: Web interface for human reviewers (future)
- **CLI Tools**: Command-line interface for administration (future)

**Key Responsibilities:**
- Accept HTTP requests from workflow platforms
- Validate and transform input data
- Route requests to appropriate application services
- Format and return responses
- Handle authentication and authorization
- Provide user interfaces for human interaction

**Dependencies:**
- Nucleus.Application (for business logic)
- Nucleus.Infrastructure (for data access)
- External HTTP clients (n8n, Make, Power Automate)

## Key Architectural Decisions

### 1. EvaluationService Location

**Current**: `Application/Services/EvaluationService.cs`
**Rationale**: 
- Implements `IEvaluationService` interface from Domain
- Contains business logic for evaluation orchestration
- Uses Semantic Kernel as a tool, not as infrastructure
- The actual OpenAI API call is abstracted through Semantic Kernel

**Alternative Consideration**: Move to Infrastructure
- **Pros**: OpenAI is external infrastructure
- **Cons**: Would violate dependency direction (Infrastructure → Application)
- **Decision**: Keep in Application as it's business logic using external tools

### 2. Prompts Location

**Current**: `Application/Prompts/`
**Rationale**:
- Prompts are business rules and domain knowledge
- They define how evaluation should work
- They're part of the evaluation business logic
- They're not infrastructure-specific

**Alternative Consideration**: Move to Domain
- **Pros**: Prompts are core business knowledge
- **Cons**: Domain should be pure business logic without external dependencies
- **Decision**: Keep in Application as they're part of the evaluation use case

### 3. Configuration Location

**Current**: `Application/Services/OpenAIConfiguration.cs`
**Rationale**:
- Configuration is a cross-cutting concern
- Application layer needs configuration to orchestrate services
- Keeps configuration close to where it's used

**Alternative**: Move to Infrastructure
- **Pros**: Configuration is infrastructure concern
- **Cons**: Would require dependency injection complexity
- **Decision**: Keep in Application for simplicity, but consider Infrastructure for complex config

## Dependency Rules

### Dependency Direction
```
Interface → Application → Domain
Infrastructure → Application → Domain
```

**Key Principles**:
1. Dependencies point inward toward Domain
2. Domain has no dependencies on other layers
3. Application depends only on Domain
4. Infrastructure implements Domain/Application interfaces
5. Interface depends on Application for business logic

### Dependency Injection
- **Domain**: Defines interfaces
- **Application**: Implements business logic and registers services
- **Infrastructure**: Implements external concerns
- **Interface**: Configures DI container

## File Organization Rules

### 1. Feature-Based Organization
```
Features/
├── FeatureName/
│   ├── Commands/
│   ├── Queries/
│   ├── Handlers/
│   ├── Validators/
│   └── DTOs/
```

### 2. Service Organization
```
Services/
├── Domain Services (interfaces)
├── Application Services (implementations)
└── Infrastructure Services (external implementations)
```

### 3. Configuration Organization
```
Configuration/
├── Domain Config (business rules)
├── Application Config (use case settings)
└── Infrastructure Config (external service settings)
```

## Cross-Cutting Concerns

### 1. Logging
- **Domain**: No logging (pure business logic)
- **Application**: Structured logging for business events
- **Infrastructure**: Technical logging for external calls
- **Interface**: Request/response logging

### 2. Validation
- **Domain**: Business rule validation
- **Application**: Input validation (FluentValidation)
- **Interface**: Request validation

### 3. Error Handling
- **Domain**: Domain exceptions
- **Application**: Application exceptions
- **Infrastructure**: Technical exceptions
- **Interface**: HTTP error responses

## Testing Strategy

### 1. Unit Tests
- **Domain**: Test business logic in isolation
- **Application**: Test use cases with mocked dependencies
- **Infrastructure**: Test external service integrations

### 2. Integration Tests
- **Application + Infrastructure**: Test real implementations
- **API Tests**: Test complete request/response cycles

### 3. Test Organization
```
Tests/
├── Unit/
│   ├── Domain/
│   ├── Application/
│   └── Infrastructure/
└── Integration/
    ├── Application/
    └── API/
```

## Evolution Guidelines

### When to Move Code Between Layers

**Move to Domain**:
- Business rules that are core to the domain
- Entities and value objects
- Domain events

**Move to Application**:
- Use case orchestration
- Command/query handlers
- Application services

**Move to Infrastructure**:
- External API integrations
- Database access
- File system operations

**Move to Interface**:
- HTTP concerns
- Serialization
- API documentation

### Refactoring Triggers
1. **Dependency Violations**: When dependencies point in wrong direction
2. **Mixed Responsibilities**: When a class has multiple concerns
3. **Framework Coupling**: When business logic depends on frameworks
4. **Testability Issues**: When code is hard to test in isolation

## Current Architecture Assessment

### Strengths
- Clear separation of concerns
- Proper dependency direction
- Feature-based organization
- Good use of CQRS pattern

### Areas for Improvement
- Consider moving complex configuration to Infrastructure
- Evaluate if some Application services should be in Infrastructure
- Ensure all external dependencies are properly abstracted

### Future Considerations
- Event sourcing for audit trails
- CQRS with separate read/write models
- Microservices decomposition
- API versioning strategy

## Conclusion

The current architecture follows clean architecture principles well. The placement of EvaluationService in Application and Prompts in Application is appropriate because:

1. **EvaluationService**: Contains business logic for evaluation orchestration, not just infrastructure calls
2. **Prompts**: Represent business rules and domain knowledge for evaluation
3. **Dependencies**: Maintain proper dependency direction
4. **Testability**: Allows for easy testing and mocking

The key is maintaining the dependency rules and ensuring each layer has a single, well-defined responsibility. 