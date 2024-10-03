namespace PersonCatalog.Application.PersonDirectory.Commands.DeletePerson;

public class DeletePersonHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeletePersonCommand, DeletePersonResult>
{
    public async Task<DeletePersonResult> Handle(DeletePersonCommand command, CancellationToken cancellationToken)
    {
        var personId = PersonId.Of(command.Id);
        var person = await dbContext.Persons.FindAsync([personId], cancellationToken: cancellationToken);

        if (person is null)
        {
            throw new PersonNotFoundException(command.Id);
        }

        dbContext.Persons.Remove(person);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeletePersonResult(true);
    }
}
