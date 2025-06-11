# HTTP Client

Abstraction for making HTTP requests using RestSharp.

## Contents

- `IRootsHttpClient`: Interface for HTTP operations.
- `RootsHttpClient`: Implementation for sending HTTP requests.

## Usage

- Inject `IRootsHttpClient` to perform HTTP operations.
- Configure base URL and other settings as needed.

### Sample: Register and Use HTTP Client

```csharp
services.AddRootsHttpClient(Configuration);

public class ExternalApiService
{
    private readonly IRootsHttpClient _httpClient;
    public ExternalApiService(IRootsHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDataAsync()
    {
        var response = await _httpClient.GetAsync<string>("/api/data");
        return response;
    }
}
```

---

**Directory:** `Roots.Framework/Externals/Http/`
