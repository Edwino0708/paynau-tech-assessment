using Microsoft.AspNetCore.Mvc;

namespace PersonCatalog.API.EndPoints;

public record CreatePersonRequest(PersonDto Person);
public record CreatePersonReponse(Guid id);

public class CreatePerson : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/persons", async ([FromBody] CreatePersonRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreatePersonCommand>();
            var result = await sender.Send(command);

            var response = result.Adapt<CreatePersonReponse>();

            return Results.Created($"/created/{response.id}", response);
        })
            .WithName("CreatePerson")
            .Produces<CreatePersonReponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Person")
            .WithDescription("Create Person");
    }
}
