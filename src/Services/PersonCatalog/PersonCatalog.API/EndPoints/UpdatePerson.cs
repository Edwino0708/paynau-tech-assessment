using PersonCatalog.Application.PersonDirectory.Commands.UpdatePerson;

namespace PersonCatalog.API.EndPoints;

public record UpdatePersonRequest(PersonDto Person);

public record UpdatePersonReponse(bool IsSuccess);

public class UpdatePerson : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/persons/", async ([AsParameters] UpdatePersonRequest request, ISender sender) => 
        {
            var result = await sender.Send(new UpdatePersonCommand(request.Person));
            var response = result.Adapt<UpdatePersonReponse>();

            return Results.Ok(response);
        })
            .WithName("UpdatePerson")
            .Produces<UpdatePersonReponse>(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Person")
            .WithDescription("Update Person");
    }
}
