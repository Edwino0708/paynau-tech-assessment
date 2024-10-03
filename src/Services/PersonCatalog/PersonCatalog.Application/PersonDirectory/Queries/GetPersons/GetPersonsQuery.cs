namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersons;

public record GetPersonsQuery(PaginatedRequest PaginateRequest)
    : IQuery<GetPersonsResult>;

public record GetPersonsResult(PaginateResult<PersonDto> Persons);