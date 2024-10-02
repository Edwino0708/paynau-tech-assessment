
namespace PersonCatalog.Application.PersonDirectory.Commands.CreatePerson;

public class CreatePersonHandler()
    : ICommandHandler<CreatePersonCommand, CreatePersonResult>

{
    public Task<CreatePersonResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {

        throw new NotImplementedException();
    }
}
