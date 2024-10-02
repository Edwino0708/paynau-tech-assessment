namespace PersonCatalog.Domain.ValueObjects;

public record PersonId
{
    public Guid Value { get; }

    private PersonId(Guid value) => Value = value;

    public static PersonId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainExcepetion("PersonId cannot be empty.");
        }

        return new PersonId(value);
    }
}
