using System.Text;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using Roots.Framework.CQRS.Behaviors;
using Roots.Framework.Externals.Http;
using Roots.Framework.Externals.Messaging;
using Roots.Framework.Middleware;
using Roots.Framework.Persistence.UnitOfWork;
using Roots.Framework.Settings;

namespace Roots.Framework.Configuration;

public static class ServiceExtension
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services, IConfiguration? configuration = null)
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

    public static IServiceCollection AddRootsJWT(this IServiceCollection services, IConfiguration? configuration = null)
    {
        var jwtSettings = configuration?.GetSection("JwtSettings").Get<JwtSettings>();
        
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
        
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