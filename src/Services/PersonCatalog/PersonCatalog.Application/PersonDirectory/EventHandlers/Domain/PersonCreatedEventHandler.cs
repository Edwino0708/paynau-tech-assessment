//namespace Ordering.Application.Orders.EventHandlers.Domain;

//public class PersonCreatedEventHandler
//    (IPublishEndpoint publishEndpoint, IFeatureManager featureManager, ILogger<PersonCreatedEventHandler> logger)
//    : INotificationHandler<PersonCreatedEvent>
//{
//    public async Task Handle(PersonCreatedEvent domainEvent, CancellationToken cancellationToken)
//    {
//        logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType().Name);

//        var orderCreatedIntegrationEvent = domainEvent.person.ToPersonDto();
//        await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);       
//    }
//}
