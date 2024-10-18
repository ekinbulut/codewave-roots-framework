using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Roots.Framework.Middleware;

public class LoggingMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Generate a unique transaction ID
        var transactionId = Guid.NewGuid().ToString();

        // Add the transaction ID to the response headers
        context.Response.Headers.Add("X-Transaction-ID", transactionId);

        // Add the transaction ID to the log scope
        using (_logger.BeginScope(new { TransactionId = transactionId }))
        {
            _logger.LogInformation(
                $"Request: {context.Request.Method} {context.Request.Path}, Transaction ID: {transactionId}");

            await _next(context);

            _logger.LogInformation($"Response: {context.Response.StatusCode}, Transaction ID: {transactionId}");
        }
    }
}