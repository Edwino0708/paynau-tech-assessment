using Microsoft.Extensions.DependencyInjection;
using PersonCatalog.Application.Services;
using PersonCatalog.Infrastructure.Services;
using System;

namespace PersonCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database");
        string redisConnection = configuration["Redis:ConnectionString"];

        // Register the interceptors as scoped services
        services.AddScoped<AuditableEntityInterceptor>(); // Ensure this is registered
        services.AddScoped<DispatchDomainEventsInterceptor>(); // Ensure this is registered

        // Add the SaveChangesInterceptor interface
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseMySql(connectionString, 
                ServerVersion.AutoDetect(connectionString),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null));

            // Resolve interceptors here
            var auditableEntityInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            var dispatchDomainEventsInterceptor = sp.GetRequiredService<DispatchDomainEventsInterceptor>();

            // Add interceptors to the DbContext options
            options.AddInterceptors(auditableEntityInterceptor);
            options.AddInterceptors(dispatchDomainEventsInterceptor);
        });

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
       
        services.AddScoped<IDatabase>(provider =>
            provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IPersonReadRepository, PersonReadRepository>();
        services.AddScoped<IPersonWriteRepository, PersonWriteRepository>();

        services.AddScoped<ICacheService, CacheService>();


        return services;
    }
}
