# CQRS

This module provides interfaces and pipeline behaviors for implementing the Command Query Responsibility Segregation (CQRS) pattern.

## Contents

- `ICommand` / `IQuery`: Marker interfaces for commands and queries.
- `BaseResponse`: Standard response wrapper for CQRS operations.
- `Behaviors/`: MediatR pipeline behaviors for logging, trace ID propagation, and global exception handling.

## Usage

- Implement `ICommand<TResult>` or `IQuery<TResult>` for your requests.
- Register MediatR and the provided behaviors in your DI container.

### Sample: Define and Handle a Command

```csharp
public class CreateUserCommand : ICommand<BaseResponse>
{
    public string UserName { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResponse>
{
    public Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Handle command logic
        return Task.FromResult(new BaseResponse { Success = true });
    }
}
```

### Sample: Register MediatR and Behaviors

```csharp
services.AddRootMediatr(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly);
});
```

---

**Directory:** `Roots.Framework/CQRS/`
