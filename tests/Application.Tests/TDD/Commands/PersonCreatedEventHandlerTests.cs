namespace Application.Tests.TDD.Commands;

[Trait("EventHandler", "PersonCreated")]
public class PersonCreatedEventHandlerTests
{
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly Mock<IFeatureManager> _featureManagerMock;
    private readonly Mock<ILogger<PersonCreatedEventHandler>> _loggerMock;
    private readonly PersonCreatedEventHandler _handler;

    public PersonCreatedEventHandlerTests()
    {
        _publishEndpointMock = new Mock<IPublishEndpoint>();
        _featureManagerMock = new Mock<IFeatureManager>();
        _loggerMock = new Mock<ILogger<PersonCreatedEventHandler>>();
        _handler = new PersonCreatedEventHandler(_publishEndpointMock.Object, _featureManagerMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Given_ValidEvent_When_HandleCalled_Should_PublishEvent()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var fullName = "John Doe";
        var person = Person.Create(PersonId.Of(personId), fullName, DateTime.Now, "john@example.com", "123456789", "Address", "Male", "Nationality", "Occupation");
        var domainEvent = new PersonCreatedEvent(person);

        // Act
        await _handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        _publishEndpointMock.Verify(x => x.Publish(It.Is<PersonDto>(dto =>
            dto.Id == personId && dto.FullName == fullName), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Given_ValidEvent_When_HandleCalled_Should_LogEvent()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var fullName = "John Doe";
        var person = Person.Create(PersonId.Of(personId), fullName, DateTime.Now, "john@example.com", "123456789", "Address", "Male", "Nationality", "Occupation");
        var domainEvent = new PersonCreatedEvent(person);

        // Variable para capturar el mensaje loggeado
        var capturedLogMessage = string.Empty;

        // Ajustamos el logger para capturar el mensaje de log
        _loggerMock
            .Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true), // Aceptamos cualquier mensaje
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ))
            .Callback<LogLevel, EventId, object, Exception, Delegate>((logLevel, eventId, state, exception, formatter) =>
            {
                capturedLogMessage = state.ToString(); // Capturamos el mensaje
            });

        // Act
        await _handler.Handle(domainEvent, CancellationToken.None);

        // Assert
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()
            ), Times.Once);

        // Verificamos que el mensaje contiene el nombre completo
        Assert.Contains("Domain Event handled: PersonCreatedEvent", capturedLogMessage);
    }
}
