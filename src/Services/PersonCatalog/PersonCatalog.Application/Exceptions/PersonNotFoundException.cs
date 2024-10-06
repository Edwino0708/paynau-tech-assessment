namespace PersonCatalog.Application.Exceptions;

public class PersonNotFoundException : NotFoundException
{
    public Guid PersonId { get; }

    public PersonNotFoundException(Guid personId)
        : base($"Person with ID {personId} not found.")
    {
        PersonId = personId;
    }
}
