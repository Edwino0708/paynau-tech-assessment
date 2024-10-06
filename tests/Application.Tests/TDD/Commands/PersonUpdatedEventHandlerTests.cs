namespace Application.Tests.TDD.Commands;

[Trait("EventHandler", "PersonUpdated")]
public class PersonUpdatedEventHandlerTests
{
    private Mock<IPersonReadRepository> _personReadRepositoryMock;
    private Mock<IPersonWriteRepository> _personWriteRepositoryMock;
    private Mock<ICacheService> _cacheServiceMock;
    private Mock<ILogger<UpdatePersonHandler>> _loggerMock;

    public PersonUpdatedEventHandlerTests()
    {
        // Inicializa los mocks
        _personReadRepositoryMock = new Mock<IPersonReadRepository>();
        _personWriteRepositoryMock = new Mock<IPersonWriteRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _loggerMock = new Mock<ILogger<UpdatePersonHandler>>();
    }

    [Fact]
    public async Task Given_InvalidPersonId_When_HandleCalled_Should_ThrowPersonNotFoundException()
    {
        // Arrange
        var invalidPersonId = Guid.NewGuid(); // ID que no existe
        var personDto = new PersonDto(
            Id: invalidPersonId,
            FullName: "Jane Doe",
            DateOfBirth: DateTime.Now,
            Email: "jane@example.com",
            PhoneNumber: "987654321",
            Address: "Another Address",
            Gender: "Female",
            Nationality: "Nationality",
            Occupation: "Occupation"
        );

        var command = new UpdatePersonCommand(personDto);
        var handler = new UpdatePersonHandler(_personReadRepositoryMock.Object, _personWriteRepositoryMock.Object, _cacheServiceMock.Object, _loggerMock.Object);

        // Setup read repository to indicate that the person does not exist
        _personReadRepositoryMock.Setup(repo => repo.ExistsAsync(PersonId.Of(invalidPersonId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<PersonNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        Assert.Equal(invalidPersonId, exception.PersonId); // Verifica que el ID de la excepción es el esperado
    }
}