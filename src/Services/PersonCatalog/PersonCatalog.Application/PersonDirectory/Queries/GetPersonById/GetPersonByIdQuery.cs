namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersonById;

public  record GetPersonByIdQuery(Guid Id)
        : IQuery<GetPersonByIdQueryResult>;

public record GetPersonByIdQueryResult(PersonDto Person);