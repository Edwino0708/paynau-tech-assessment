using PersonCatalog.Infrastructure.Services;

namespace PersonCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database");
        string redisConnection = configuration["Redis:ConnectionString"];

        services.AddSingleton<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseMySql(connectionString, 
                ServerVersion.AutoDetect(connectionString),
                options => options.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: System.TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null));

            options.AddInterceptors(sp.GetRequiredService<ISaveChangesInterceptor>());
        });

        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
       
        services.AddScoped<IDatabase>(provider =>
            provider.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<ICacheService, CacheService>();


        return services;
    }
}
