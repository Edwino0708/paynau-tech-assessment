namespace PersonCatalog.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks()
           .AddMySql(configuration.GetConnectionString("Database")!)
           .AddRedis(configuration["Redis:ConnectionString"]);

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAnyOrigin",
              builder =>
              {
                  builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
              });
        });

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseRouting();
        app.UseCors("AllowAnyOrigin");
       
        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health", new HealthCheckOptions 
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        app.MapCarter();

        return app;
    }

}
