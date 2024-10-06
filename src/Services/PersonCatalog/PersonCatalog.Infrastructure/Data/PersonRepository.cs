namespace PersonCatalog.Infrastructure.Data;

public class PersonReadRepository : IPersonReadRepository
{
    private readonly IApplicationDbContext _dbContext;

    public PersonReadRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Person> GetByIdAsync(PersonId personId, CancellationToken cancellationToken)
    {
        return await _dbContext.Persons.FirstOrDefaultAsync(x => x.Id == personId, cancellationToken);
    }

    public async Task<Person> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Persons.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> ExistsAsync(PersonId personId, CancellationToken cancellationToken)
    {
        return await _dbContext.Persons.AnyAsync(x => x.Id == personId, cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Persons.CountAsync(cancellationToken);
    }

    public async Task<List<Person>> GetPagedAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        return await _dbContext.Persons
            .OrderBy(o => o.FullName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
}

public class PersonWriteRepository : IPersonWriteRepository
{
    private readonly IApplicationDbContext _dbContext;

    public PersonWriteRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateAsync(Person person, CancellationToken cancellationToken)
    {
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
        await _dbContext.Persons.AddAsync(person, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PersonId personId, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Persons.FirstOrDefaultAsync(x => x.Id == personId, cancellationToken);
        if (person != null)
        {
            _dbContext.Persons.Remove(person);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}