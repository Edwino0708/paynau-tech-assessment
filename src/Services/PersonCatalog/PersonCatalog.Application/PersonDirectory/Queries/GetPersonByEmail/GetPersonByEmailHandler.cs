namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersonByEmail;

public class GetPersonByEmailHandler(IPersonReadRepository personReadRepository, ILogger<GetPersonByEmailHandler> logger)
        : IQueryHandler<GetPersonByEmailQuery, GetPersonByEmailQueryResult>

{
    public async Task<GetPersonByEmailQueryResult> Handle(GetPersonByEmailQuery query, CancellationToken cancellationToken)
    {
        var person = await personReadRepository.GetByEmailAsync(query.Email, cancellationToken);
        logger.LogInformation($"Get Person Information by Email : {query.Email}");
        return new GetPersonByEmailQueryResult(person?.ToPersonDto());
    }
}
