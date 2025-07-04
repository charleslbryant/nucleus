# ADR-004: Semantic Kernel for LLM Integration

## Status
**Accepted**

## Context
Nucleus needs to integrate with Large Language Models (LLMs) for automated evaluation of AI outputs. The integration must be reliable, maintainable, and support multiple LLM providers while providing a consistent interface for prompt management and response handling.

Forces at play:
- Need for reliable LLM integration with error handling
- Requirement for consistent prompt management
- Support for multiple LLM providers (OpenAI, Azure OpenAI, etc.)
- Integration with .NET ecosystem
- Response parsing and validation complexity
- Future extensibility for different AI services

## Decision
We will use Microsoft Semantic Kernel for LLM integration in Nucleus.

Key aspects of this decision:
- **Semantic Kernel 1.0+**: Latest stable version with improved APIs
- **OpenAI Connector**: Primary integration with OpenAI GPT models
- **Prompt Management**: Structured prompt templates and management
- **Error Handling**: Built-in retry logic and error management
- **Response Parsing**: Robust parsing of LLM responses
- **Extensibility**: Easy to add new AI services and providers

## Consequences

### Positive
- **Ecosystem Integration**: Native .NET integration with excellent C# support
- **Provider Flexibility**: Easy to switch between OpenAI, Azure OpenAI, and others
- **Prompt Management**: Structured approach to prompt engineering
- **Error Handling**: Built-in retry logic and error management
- **Response Parsing**: Robust parsing with multiple format support
- **Future-Proof**: Microsoft-backed with active development
- **Community**: Growing community and documentation
- **Performance**: Optimized for .NET performance characteristics

### Negative
- **Learning Curve**: Team must understand Semantic Kernel concepts
- **Dependency**: Tied to Microsoft's Semantic Kernel roadmap
- **Complexity**: Additional abstraction layer over direct API calls
- **Versioning**: Rapid development may require frequent updates
- **Limited Providers**: Fewer providers compared to some alternatives

### Neutral
- **Licensing**: MIT license, open source
- **Maturity**: Relatively new but well-supported by Microsoft

## Rationale
Semantic Kernel was chosen over alternatives like:
- **Direct OpenAI SDK**: More boilerplate, less structured approach
- **LangChain**: Python-focused, different ecosystem
- **Custom Implementation**: Would require significant development effort
- **Other .NET Libraries**: Less mature or less feature-rich

The decision leverages Microsoft's investment in AI tooling while providing excellent .NET integration. Semantic Kernel's structured approach to prompt management and response handling aligns well with our clean architecture principles.

## Related Decisions
- [ADR-002: ASP.NET Core Framework](ADR-002-aspnet-core.md)
- [ADR-001: Clean Architecture Pattern](ADR-001-clean-architecture.md)

## References
- [Semantic Kernel Documentation](https://learn.microsoft.com/en-us/semantic-kernel/)
- [OpenAI Connector Guide](https://learn.microsoft.com/en-us/semantic-kernel/ai-connectors/openai)
- [Semantic Kernel GitHub Repository](https://github.com/microsoft/semantic-kernel) 