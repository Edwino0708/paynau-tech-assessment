namespace PersonCatalog.Domain.Events;

public record PersonUpdatedEvent(Person person) : IDomainEvent;
