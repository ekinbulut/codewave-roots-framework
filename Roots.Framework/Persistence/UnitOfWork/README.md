# Unit of Work

Implements the Unit of Work pattern for managing database transactions.

## Contents

- `IUnitOfWork`: Interface for transactional operations.
- `UnitOfWork`: Implementation for managing EF Core DbContext transactions.
- `IReadonlyUnitOfWork` / `ReadonlyUnitOfWork`: For read-only operations.

## Usage

- Inject `IUnitOfWork` into your services to manage transactions.
- Call `CommitAsync()` to persist changes.

### Sample: Register and Use UnitOfWork

```csharp
services.AddUnitOfWork(Configuration);

public class UserService
{
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SaveUserAsync(User user)
    {
        // ... add/update user ...
        await _unitOfWork.CommitAsync();
    }
}
```

---

**Directory:** `Roots.Framework/Persistence/UnitOfWork/`
