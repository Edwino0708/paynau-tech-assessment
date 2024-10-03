namespace PersonCatalog.API.EndPoints;

public record GetPersonsResponse(PaginateResult<PersonDto> Persons);

public class GetPersons : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/persons", async ([AsParameters] PaginatedRequest request, ISender sender) => 
        {
            var result = await sender.Send(new GetPersonsQuery(request));
            var response = result.Adapt<GetPersonsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetPersons")
        .Produces<GetPersonsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Persons")
        .WithDescription("Get Persons");
    }
}
