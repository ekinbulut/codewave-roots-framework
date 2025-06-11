# Middleware

Custom ASP.NET Core middleware for error handling, logging, and request culture.

## Contents

- `ErrorHandlerMiddleware`: Global error handler.
- `LoggingMiddleware`: Logs requests and responses with transaction IDs.
- `RequestCultureMiddleware`: Sets request culture based on headers.

## Usage

- Register middleware in your `Startup.cs` or Program file.

### Sample: Register Middleware

```csharp
app.UseRootErrorHandling();
app.UseRootLogging();
app.UseRequestCulture();
```

---

**Directory:** `Roots.Framework/Middleware/`
