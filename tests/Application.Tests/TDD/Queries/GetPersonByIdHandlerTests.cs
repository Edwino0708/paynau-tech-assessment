using PersonCatalog.Application.PersonDirectory.Queries.GetPersonById;

namespace Application.Tests.TDD.Queries;

public class GetPersonByIdHandlerTests
{
    private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
    private readonly Mock<ILogger<GetPersonByIdHandler>> _loggerMock;
    private readonly GetPersonByIdHandler _handler;

    public GetPersonByIdHandlerTests()
    {
        _personReadRepositoryMock = new Mock<IPersonReadRepository>();
        _loggerMock = new Mock<ILogger<GetPersonByIdHandler>>();
        _handler = new GetPersonByIdHandler(_personReadRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var person = Person.Create(
            PersonId.Of(personId),
            "John Doe",
            DateTime.Now.AddYears(-30),
            "john.doe@example.com",
            "1234567890",
            "123 Main St",
            "Male",
            "American",
            "Software Developer"
        );

        _personReadRepositoryMock.Setup(repo => repo.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);

        var query = new GetPersonByIdQuery(personId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Person.Should().NotBeNull();
        result.Person.FullName.Should().Be("John Doe");

        // Verify logger is called
        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, type) => @object.ToString().Contains(personId.ToString())),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = Guid.NewGuid();

        _personReadRepositoryMock.Setup(repo => repo.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null);

        var query = new GetPersonByIdQuery(personId);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Person.Should().BeNull();

        // Verify logger is called
        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, type) => @object.ToString().Contains(personId.ToString())),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldLogInformation_WhenPersonIsFound()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var person = Person.Create(
            PersonId.Of(personId),
            "Jane Doe",
            DateTime.Now.AddYears(-25),
            "jane.doe@example.com",
            "0987654321",
            "456 Elm St",
            "Female",
            "Canadian",
            "Product Manager"
        );

        _personReadRepositoryMock.Setup(repo => repo.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);

        var query = new GetPersonByIdQuery(personId);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, type) => @object.ToString().Contains(personId.ToString())),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldLogInformation_WhenPersonDoesNotExist()
    {
        // Arrange
        var personId = Guid.NewGuid();

        _personReadRepositoryMock.Setup(repo => repo.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null);

        var query = new GetPersonByIdQuery(personId);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _loggerMock.Verify(logger => logger.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((@object, type) => @object.ToString().Contains(personId.ToString())),
            null,
            It.IsAny<Func<It.IsAnyType, Exception, string>>()),
        Times.Once);
    }
}