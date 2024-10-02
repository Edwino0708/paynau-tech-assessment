namespace PersonCatalog.Domain.Events;

public record PersonCreatedEvent(Person person) : IDomainEvent;