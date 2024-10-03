namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersonByEmail;

public class GetPersonByEmailHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetPersonByEmailQuery, GetPersonByEmailQueryResult>

{
    public async Task<GetPersonByEmailQueryResult> Handle(GetPersonByEmailQuery query, CancellationToken cancellationToken)
    {
        var person = await dbContext.Persons.FirstOrDefaultAsync(p => p.Email == query.Email);
        return new GetPersonByEmailQueryResult(person.ToPersonDto());
    }
}
