# ADR-002: ASP.NET Core Framework

## Status
**Accepted**

## Context
Nucleus requires a robust, performant web framework to handle HTTP requests from multiple workflow platforms. The framework must support modern development practices, provide excellent tooling, and integrate well with our chosen technology stack.

Forces at play:
- Need for high-performance HTTP handling
- Requirement for modern development practices
- Integration with .NET ecosystem
- Cross-platform deployment capability
- Team expertise and familiarity
- Long-term Microsoft support and roadmap

## Decision
We will use ASP.NET Core as our primary web framework for Nucleus.

Key aspects of this decision:
- **ASP.NET Core 8.0+**: Latest LTS version with performance improvements
- **Minimal API approach**: For simple endpoints like `/api/evaluate`
- **Built-in dependency injection**: For clean service management
- **Middleware pipeline**: For cross-cutting concerns like validation and error handling
- **OpenAPI/Swagger support**: For API documentation and testing
- **Cross-platform**: Runs on Windows, Linux, and macOS

## Consequences

### Positive
- **Performance**: High-performance HTTP pipeline with Kestrel server
- **Modern Development**: Hot reload, minimal APIs, and modern C# features
- **Ecosystem Integration**: Seamless integration with Entity Framework, Semantic Kernel
- **Tooling**: Excellent Visual Studio and VS Code support
- **Cross-Platform**: Deployable on any platform
- **Long-term Support**: Microsoft-backed with clear roadmap
- **Community**: Large, active developer community

### Negative
- **Learning Curve**: Team must be familiar with .NET ecosystem
- **Platform Lock-in**: Tied to Microsoft ecosystem
- **Deployment Complexity**: Requires .NET runtime on target platforms
- **Memory Usage**: Higher memory footprint compared to lightweight alternatives

### Neutral
- **Licensing**: Open source with MIT license
- **Maturity**: Well-established framework with proven track record

## Rationale
ASP.NET Core was chosen over alternatives like:
- **Node.js/Express**: Less type safety, different ecosystem
- **Python/FastAPI**: Different language, less integration with chosen tools
- **Go/Gin**: Different language, smaller ecosystem
- **Java/Spring**: More verbose, different ecosystem

The decision leverages team expertise in C# and .NET while providing excellent performance, modern development experience, and seamless integration with our chosen tools (Entity Framework, Semantic Kernel).

## Related Decisions
- [ADR-001: Clean Architecture Pattern](ADR-001-clean-architecture.md)
- [ADR-004: Semantic Kernel for LLM Integration](ADR-004-semantic-kernel.md)

## References
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [.NET 8 Performance Improvements](https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-8/) 