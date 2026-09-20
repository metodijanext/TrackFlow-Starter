# ADR 0001: CQRS Pattern Selection

## Status
ACCEPTED

## Context
We need to implement a scalable, maintainable architecture for the TrackFlow time tracking system. The system must support:
- Clear separation between read and write operations
- Independent scaling of queries vs commands
- Easy testing of business logic
- Future extensibility

## Decision
We will use **CQRS (Command Query Responsibility Segregation)** pattern combined with **MediatR** for command/query routing.

## Rationale
- **Separation of Concerns**: Commands (writes) and Queries (reads) are handled separately
- **Scalability**: Read and write operations can scale independently
- **Testability**: Each handler can be tested in isolation
- **Clear Intent**: Code intent is explicit - is this a read or write?
- **MediatR**: Excellent .NET library for implementing CQRS with dependency injection

## Implementation
```csharp
// Example: Login Command (Write)
public class LoginCommand : IRequest<AuthResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // Business logic: verify email, hash password, generate JWT
    }
}

// Example: GetUser Query (Read)
public class GetUserByIdQuery : IRequest<UserDto>
{
    public Guid UserId { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Business logic: fetch and return user
    }
}
```

## Alternatives Considered
1. **Traditional Repository Pattern**
   - Pros: Simpler to understand
   - Cons: Mixes read/write, harder to scale, less testable

2. **Event Sourcing**
   - Pros: Complete audit trail, replay events
   - Cons: Too complex for this educational course

3. **GraphQL with Resolvers**
   - Pros: Flexible querying
   - Cons: Adds complexity, client-side complexity

## Consequences
- **Positive**: Clear separation, easy testing, explicit intent
- **Negative**: More files/classes, slight boilerplate overhead
- **Mitigation**: Templates and code generation reduce boilerplate

## Related ADRs
- ADR 0002: Clean Architecture Layers
- ADR 0005: React TypeScript Frontend

---

*This ADR explains why we structure our code with separate Command and Query handlers.*
