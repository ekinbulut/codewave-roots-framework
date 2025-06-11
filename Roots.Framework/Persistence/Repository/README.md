# Repository Pattern

Provides generic repository interfaces and base implementations for data access using Entity Framework Core.

## Contents

- `IRepository<TEntity, TKey>`: Generic repository interface.
- `BaseRepository<TEntity, TKey>`: Base implementation for CRUD operations.
- `IReadonlyRepository<TEntity, TKey>`: Read-only repository interface.
- `BaseReadonlyRepository<TEntity, TKey>`: Base implementation for read-only operations.

## Usage

- Inherit from `BaseRepository` or `BaseReadonlyRepository` for your entity repositories.
- Register repositories in your DI container.

### Sample: Implement a Repository

```csharp
public class UserRepository : BaseRepository<User, Guid>
{
    public UserRepository(DbContext context) : base(context) { }
}
```

### Sample: Register and Use Repository

```csharp
services.AddScoped<IRepository<User, Guid>, UserRepository>();

// Inject and use in your service
public class UserService
{
    private readonly IRepository<User, Guid> _userRepository;
    public UserService(IRepository<User, Guid> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
}
```

---

**Directory:** `Roots.Framework/Persistence/Repository/`
