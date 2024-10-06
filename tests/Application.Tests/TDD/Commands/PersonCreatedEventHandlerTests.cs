using PersonCatalog.Application.PersonDirectory.Commands.CreatePerson;

namespace Application.Tests.TDD.Commands;

[Trait("EventHandler", "PersonCreated")]
public class PersonCreatedEventHandlerTests
{
    private readonly Mock<IPersonWriteRepository> _personWriteRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly Mock<ILogger<CreatePersonHandler>> _loggerMock;
    private readonly CreatePersonHandler _handler;

    public PersonCreatedEventHandlerTests()
    {
        _personWriteRepositoryMock = new Mock<IPersonWriteRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _loggerMock = new Mock<ILogger<CreatePersonHandler>>();
        _handler = new CreatePersonHandler(_personWriteRepositoryMock.Object, _cacheServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreatePerson_AndReturnResult()
    {
        // Arrange
        var command = new CreatePersonCommand
        (
            new PersonDto
            (
                Id: new Guid(),
                FullName:"John Doe",
                DateOfBirth: new DateTime(1990, 1, 1),
                Email: "john.doe@example.com",
                PhoneNumber: "1234567890",
                Address: "123 Main St",
                Gender: GenderStatus.MALE,
                Nationality: "American",
                Occupation:"Engineer"
            )
        );

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _personWriteRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Person>(), It.IsAny<CancellationToken>()), Times.Once);
        _cacheServiceMock.Verify(service => service.CleanAllAsync(), Times.Once);
        _loggerMock.Verify(logger => logger.LogInformation(It.IsAny<string>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }
}
