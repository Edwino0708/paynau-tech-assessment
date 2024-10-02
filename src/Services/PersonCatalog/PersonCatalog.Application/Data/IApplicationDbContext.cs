namespace PersonCatalog.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Person> Persons { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
