namespace PersonCatalog.Application.PersonDirectory.Queries.GetPersons;

public class GetPersonsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetPersonsQuery, GetPersonsResult>
{
    public async Task<GetPersonsResult> Handle(GetPersonsQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginateRequest.PageIndex;
        var pageSize = query.PaginateRequest.PageSize;

        var totalCount = await dbContext.Persons.LongCountAsync(cancellationToken);

        var persons = await dbContext.Persons
          .OrderBy(o => o.FullName)
          .Skip(pageSize * pageIndex)
          .Take(pageSize)
          .ToListAsync();

        return new GetPersonsResult(
           new PaginateResult<PersonDto>(
               pageIndex,
               pageSize,
               totalCount,
               persons.ToPersonDtoList()
               )
           );
    }
}
