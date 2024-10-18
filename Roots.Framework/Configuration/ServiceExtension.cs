using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Roots.Framework.CQRS.Behaviors;
using Roots.Framework.Externals.Http;
using Roots.Framework.Externals.Messaging;
using Roots.Framework.Middleware;
using Roots.Framework.Persistence.UnitOfWork;

namespace Roots.Framework.Configuration;

public static class ServiceExtension
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IReadonlyUnitOfWork, ReadonlyUnitOfWork>();
        return services;
    }
    
    public static IServiceCollection AddRootMediatr(this IServiceCollection services, Action<MediatRServiceConfiguration> configuration)
    {
        services.AddMediatR(configuration)
            .AddScoped(typeof(IRequestExceptionHandler<,,>), typeof(GlobalRequestExceptionHandler<,,>))
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        return services;
    }
    
    public static IServiceCollection AddRootsHttpClient(this IServiceCollection services, IConfiguration? configuration = null)
    {

        var configurationSection = configuration?.GetSection("Roots");
        if (configurationSection == null)
        {
            services.AddSingleton<IRestClient, RestClient>();
        }
        else  
        {
            services.AddSingleton<IRestClient>(p => new RestClient(configurationSection["BaseUrl"] ?? string.Empty));
        }
        services.AddScoped<IRootsHttpClient, RootsHttpClient>();
        return services;
    }
    
    public static IServiceCollection AddRootsMessaging(this IServiceCollection services, IConfiguration? configuration = null)
    {

        services.AddScoped<IRootsRabbitMqClient, RootsRabbitMqClient>();    

        return services;
    }
    
    public static IApplicationBuilder UseRequestCulture(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestCultureMiddleware>();
    }
    
    public static IApplicationBuilder UseRootLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggingMiddleware>();
    }
}