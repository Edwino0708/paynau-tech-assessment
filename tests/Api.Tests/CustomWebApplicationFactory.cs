using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonCatalog.Application.Data;
using PersonCatalog.Infrastructure.Data;
using System;
using System.Linq;

namespace Api.Tests;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TStartup>()
                          .UseEnvironment("Testing"); // Usar un entorno de pruebas
            });
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Eliminar el contexto de la base de datos existente
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContext));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
            string connectionString = "Server=localhost;Port=3306;Database=PersonCatalogDb;User Id=admin;Password=admin123;";

            // Agregar un contexto de base de datos en memoria (o un contenedor de pruebas)
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

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())  
            {
                var scopedServices = scope.ServiceProvider;
                // Aquí inicializa la base de datos, si es necesario
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                // Asegúrate de que la base de datos esté limpia y contenga datos necesarios para las pruebas
                db.Database.EnsureCreated();
                // Puedes agregar datos de prueba aquí si es necesario
            }
        });
    }
}
