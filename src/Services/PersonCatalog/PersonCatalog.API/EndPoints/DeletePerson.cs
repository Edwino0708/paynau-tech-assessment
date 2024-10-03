using PersonCatalog.Application.PersonDirectory.Commands.DeletePerson;

namespace PersonCatalog.API.EndPoints;

public record DeletePersonResponse(bool IsSuccess);

public class DeletePerson : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/persons/{id}", async (Guid id, ISender sender) => 
        {
            var result = await sender.Send(new DeletePersonCommand(id));
            var response = result.Adapt<DeletePersonResponse>();

            return Results.Ok(response);
        })
            .WithName("DeletePerson")
            .Produces<DeletePersonResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Person")
            .WithDescription("Delete Person");
    }
}
