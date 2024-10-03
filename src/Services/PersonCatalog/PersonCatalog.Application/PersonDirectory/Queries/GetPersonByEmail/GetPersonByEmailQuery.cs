namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersonByEmail;

public  record GetPersonByEmailQuery(string Email)
        : IQuery<GetPersonByEmailQueryResult>;

public record GetPersonByEmailQueryResult(PersonDto Person);