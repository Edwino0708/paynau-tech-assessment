namespace PersonCatalog.Application.Exceptions;

public class PersonNotFoundException : NotFoundException
{
    public PersonNotFoundException(Guid id) 
        : base("Person", id)
    {
    }

}
