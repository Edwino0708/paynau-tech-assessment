namespace PersonCatalog.Application.Data;

public interface IPersonReadRepository
{
    Task<Person> GetByIdAsync(PersonId personId, CancellationToken cancellationToken);
    Task<Person> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(PersonId personId, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
    Task<List<Person>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);
}

public interface IPersonWriteRepository
{
    Task UpdateAsync(Person person, CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken);
    Task DeleteAsync(PersonId personId, CancellationToken cancellationToken);
}