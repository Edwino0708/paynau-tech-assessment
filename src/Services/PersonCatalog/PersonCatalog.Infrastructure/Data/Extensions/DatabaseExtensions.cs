namespace PersonCatalog.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedPersonAsync(context);
    }

    private static async Task SeedPersonAsync(ApplicationDbContext context)
    {
        if (!await context.Persons.AnyAsync())
        {
            await context.Persons.AddRangeAsync(InitialData.Persons);
            await context.SaveChangesAsync();
        }
    }
}
