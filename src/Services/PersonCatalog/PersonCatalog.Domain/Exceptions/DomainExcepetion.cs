namespace PersonCatalog.Domain.Exceptions;

public class DomainExcepetion : Exception
{
    public DomainExcepetion(string message)
        : base($"Domain Excepetion \"{message}\" throws from Domain Layer.")
    {
    }
}
