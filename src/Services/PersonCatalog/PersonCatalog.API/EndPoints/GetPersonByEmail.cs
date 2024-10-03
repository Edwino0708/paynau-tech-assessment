using PersonCatalog.Application.PersonDirectory.Queries.GetPersonByEmail;

namespace PersonCatalog.API.EndPoints;

public record GetPersonByEmailResponse(PersonDto Person);

public class GetPersonByEmail : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/persons/ByEmail/{email}", async (string email, ISender sender) =>
        {
            var result = await sender.Send(new GetPersonByEmailQuery(email));
            var response = result.Adapt<GetPersonByEmailResponse>();

            return Results.Ok(response);

        }).WithName("GetPersonByEmail")
        .Produces<GetPersonByEmailResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Person By Email")
        .WithDescription("Get Person By Email");
    }
}
