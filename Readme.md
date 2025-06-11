# Roots.Framework
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Roots.Framework is a modular .NET library providing foundational building blocks for enterprise applications, including CQRS, unit of work, repository patterns, middleware, JWT authentication, HTTP/RabbitMQ clients, and more.

## Features

- **CQRS Support**: Command and Query interfaces, MediatR integration, pipeline behaviors (logging, trace ID, exception handling).
- **Persistence Layer**: Generic repository and unit of work patterns for Entity Framework Core.
- **Middleware**: Error handling, logging with transaction IDs, and request culture support.
- **Security**: JWT token generation/validation and cryptographic helpers.
- **External Integrations**: HTTP client abstraction (RestSharp) and RabbitMQ messaging client.
- **Configuration Extensions**: Easy DI registration for all features.

## Project Structure

```
Roots.Framework/
├── Common/Exceptions/         # Custom exception types
├── Configuration/             # Service registration extensions
├── CQRS/                      # CQRS interfaces and pipeline behaviors
├── Externals/
│   ├── Http/                  # HTTP client abstraction
│   └── Messaging/             # RabbitMQ client abstraction
├── Middleware/                # ASP.NET Core middleware
├── Persistence/               # Repository and UnitOfWork patterns
├── Security/                  # JWT and crypto helpers
├── Settings/                  # Strongly-typed settings classes
```

## Getting Started

1. **Install NuGet Package**

  (Publish the package to your NuGet feed and install it in your project.)

2. **Register Services in `Startup.cs` or Program**

  ```csharp
  // Add to your DI container
  services.AddUnitOfWork(Configuration);
  services.AddRootMediatr(cfg => { /* MediatR config */ });
  services.AddRootsHttpClient(Configuration);
  services.AddRootsMessaging(Configuration);
  services.AddRootsJWT(Configuration);
  ```

3. **Configure Middleware**

  ```csharp
  app.UseRootErrorHandling();
  app.UseRootLogging();
  app.UseRequestCulture();
  ```

4. **Configure Settings**

  Add relevant sections to your `appsettings.json`:

  ```json
  {
    "JwtSettings": {
     "SecretKey": "your-secret",
     "Issuer": "your-issuer",
     "Audience": "your-audience",
     "TokenExpiryInHours": 1
    },
    "RabbitMQSettings": {
     "HostName": "localhost",
     "Username": "guest",
     "Password": "guest"
    },
    "Roots": {
     "BaseUrl": "https://api.example.com"
    }
  }
  ```

## Key Components

- **CQRS**: See [`Roots.Framework.CQRS`](Roots.Framework/CQRS/) for `ICommand`, `IQuery`, and pipeline behaviors.
- **Repositories**: See [`Roots.Framework.Persistence.Repository.IRepository`](Roots.Framework/Persistence/Repository/IRepository.cs) and [`BaseRepository`](Roots.Framework/Persistence/Repository/BaseRepository.cs).
- **Unit of Work**: See [`Roots.Framework.Persistence.UnitOfWork.IUnitOfWork`](Roots.Framework/Persistence/UnitOfWork/IUnitofWork.cs) and [`UnitOfWork`](Roots.Framework/Persistence/UnitOfWork/UnitOfWork.cs).
- **JWT**: See [`Roots.Framework.Security.Jwt.TokenService`](Roots.Framework/Security/Jwt/TokenService.cs) and [`JwtSettings`](Roots.Framework/Settings/JwtSettings.cs).
- **RabbitMQ**: See [`Roots.Framework.Externals.Messaging.IRootsRabbitMqClient`](Roots.Framework/Externals/Messaging/IRootsRabbitMqClient.cs) and [`RootsRabbitMqClient`](Roots.Framework/Externals/Messaging/RootsRabbitMqClient.cs).
- **HTTP Client**: See [`Roots.Framework.Externals.Http.IRootsHttpClient`](Roots.Framework/Externals/Http/IRootsHttpClient.cs) and [`RootsHttpClient`](Roots.Framework/Externals/Http/RootsHttpClient.cs).
- **Middleware**: See [`Roots.Framework.Middleware`](Roots.Framework/Middleware/).

## Build & Test

- Build:  
  ```sh
  dotnet build
  ```
- Test:  
  ```sh
  dotnet test
  ```

## Contributing

Contributions are welcome! Please open issues or submit pull requests.

---

**License:** [MIT](LICENSE)