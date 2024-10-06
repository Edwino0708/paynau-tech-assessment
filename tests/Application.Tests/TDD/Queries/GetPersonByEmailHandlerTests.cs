namespace Application.Tests.TDD.Queries;

[Trait("Query", "GetPersonByEmail")]
public class GetPersonByEmailHandlerTests
{

    private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
    private readonly Mock<ILogger<GetPersonByEmailHandler>> _loggerMock;
    private readonly GetPersonByEmailHandler _handler;

    public GetPersonByEmailHandlerTests()
    {
        _personReadRepositoryMock = new Mock<IPersonReadRepository>();
        _loggerMock = new Mock<ILogger<GetPersonByEmailHandler>>();
        _handler = new GetPersonByEmailHandler(_personReadRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenPersonDoesNotExist()
    {
        // Arrange
        var query = new GetPersonByEmailQuery("notfound@example.com");
        _personReadRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Person.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnPerson_WhenPersonExists()
    {
        // Arrange
        var personId = PersonId.Of(Guid.NewGuid());
        var person = Person.Create(
            personId,
            "John Doe",
            new DateTime(1990, 1, 1),
            "found@example.com",
            "123456789",
            "Some Address",
            "Male",
            "American",
            "Engineer"
        );

        var query = new GetPersonByEmailQuery("found@example.com");

        _personReadRepositoryMock.Setup(repo => repo.GetByEmailAsync(query.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Person.Should().NotBeNull();
        result.Person.FullName.Should().Be("John Doe"); // Asegúrate de verificar otros campos según sea necesario
    }
}
