namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersonById;

public class GetPersonByIdHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetPersonByIdQuery, GetPersonByIdQueryResult>

{
    public async Task<GetPersonByIdQueryResult> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
    {
        var person = await dbContext.Persons.FirstOrDefaultAsync(p => p.Id == PersonId.Of(query.Id), cancellationToken);
        return new GetPersonByIdQueryResult(person.ToPersonDto());
    }
}
