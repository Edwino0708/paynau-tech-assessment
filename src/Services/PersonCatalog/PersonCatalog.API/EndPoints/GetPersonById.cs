using PersonCatalog.Application.PersonDirectory.Queries.GetPersonById;

namespace PersonCatalog.API.EndPoints;

public record GetPersonByIdResponse(PersonDto Person);

public class GetPersonById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/persons/ById/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetPersonByIdQuery(id));
            var response = result.Adapt<GetPersonByIdResponse>();

            return Results.Ok(response);

        }).WithName("GetPersonById")
        .Produces<GetPersonByEmailResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Person By Id")
        .WithDescription("Get Person By Id");
    }
}
