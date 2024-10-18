using System.Text.Json;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Roots.Framework.CQRS.Behaviors;

public class GlobalRequestExceptionHandler<TRequest, TResponse, TException> 
    : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TResponse : BaseResponse, new()
    where TException : Exception
    where TRequest : notnull
{
    private readonly ILogger<GlobalRequestExceptionHandler<TRequest, TResponse, TException>> logger;

    public GlobalRequestExceptionHandler(
        ILogger<GlobalRequestExceptionHandler<TRequest, TResponse, TException>> logger)
    {
        this.logger = logger;
    }

    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {
        var error = CreateExceptionError(exception);

        logger.LogError(JsonSerializer.Serialize(error));

        
        var response = new BaseResponse() 
        {
            Message = "An error occured.",
            HasError = true
        };

        state.SetHandled(response as TResponse);

        return Task.FromResult(response);
    }

    private static ExceptionError CreateExceptionError(TException exception)
    {
        var methodName = exception.TargetSite?.DeclaringType?.DeclaringType?.FullName;
        var message = exception.Message;
        var innerException = exception.InnerException?.Message;
        var stackTrace = exception.StackTrace;

        return new ExceptionError(methodName, message, innerException, stackTrace);
    }
}

public class ExceptionError(string? methodName, string message, string? innerException, string? stackTrace)
{

}