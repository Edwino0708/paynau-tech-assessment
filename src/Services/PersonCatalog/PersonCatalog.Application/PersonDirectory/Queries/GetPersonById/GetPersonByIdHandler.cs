namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersonById;

public class GetPersonByIdHandler(IPersonReadRepository personReadRepository, ILogger<GetPersonByIdHandler> logger)
        : IQueryHandler<GetPersonByIdQuery, GetPersonByIdQueryResult>

{
    public async Task<GetPersonByIdQueryResult> Handle(GetPersonByIdQuery query, CancellationToken cancellationToken)
    {
        var personId = PersonId.Of(query.Id);
        var person = await personReadRepository.GetByIdAsync(personId, cancellationToken);
        logger.LogInformation($"Found information of person by id:{query.Id}");

        return new GetPersonByIdQueryResult(person?.ToPersonDto());
    }
}
