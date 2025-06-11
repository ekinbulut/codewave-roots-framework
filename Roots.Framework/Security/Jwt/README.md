# JWT Security

Provides helpers for generating and validating JSON Web Tokens (JWT).

## Contents

- `TokenService`: Service for creating and validating JWTs.
- `JwtSettings`: Strongly-typed settings for JWT configuration.

## Usage

- Configure `JwtSettings` in your `appsettings.json`.
- Inject `TokenService` to issue or validate tokens.

### Sample: Configure JwtSettings in appsettings.json

```json
{
  "JwtSettings": {
    "SecretKey": "your-secret",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "TokenExpiryInHours": 1
  }
}
```

### Sample: Register and Use TokenService

```csharp
// Register in Startup.cs or Program.cs
services.AddRootsJWT(Configuration);

// Inject and use in your service or controller
public class AuthService
{
    private readonly TokenService _tokenService;
    public AuthService(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public string GenerateToken(string userId)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
        return _tokenService.GenerateToken(claims);
    }
}
```

---

**Directory:** `Roots.Framework/Security/Jwt/`
