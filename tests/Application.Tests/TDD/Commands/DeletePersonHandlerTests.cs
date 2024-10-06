namespace Application.Tests.TDD.Commands;

public class DeletePersonHandlerTests
{
    private readonly Mock<IPersonWriteRepository> _personWriteRepositoryMock;
    private readonly Mock<IPersonReadRepository> _personReadRepositoryMock; // Agregado
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly DeletePersonHandler _handler;

    public DeletePersonHandlerTests()
    {
        _personWriteRepositoryMock = new Mock<IPersonWriteRepository>();
        _personReadRepositoryMock = new Mock<IPersonReadRepository>(); // Inicializado
        _cacheServiceMock = new Mock<ICacheService>();
        _handler = new DeletePersonHandler(_personWriteRepositoryMock.Object, _personReadRepositoryMock.Object, _cacheServiceMock.Object, Mock.Of<ILogger<DeletePersonHandler>>());
    }

    [Fact]
    public async Task Handle_ShouldDeletePerson_WhenPersonExists()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var command = new DeletePersonCommand(personId);

        // Simular que el repositorio de lectura confirma que la persona existe
        _personReadRepositoryMock.Setup(repo => repo.ExistsAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        _personWriteRepositoryMock.Verify(repo => repo.DeleteAsync(PersonId.Of(personId), CancellationToken.None), Times.Once);
        _cacheServiceMock.Verify(x => x.CleanAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowPersonNotFoundException_WhenPersonDoesNotExist()
    {
        // Arrange
        var command = new DeletePersonCommand(Guid.NewGuid());

        // Simular que el repositorio de lectura no encuentra la persona
        _personReadRepositoryMock.Setup(repo => repo.ExistsAsync(It.IsAny<PersonId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<PersonNotFoundException>();
        _personWriteRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<PersonId>(), CancellationToken.None), Times.Never);
        _cacheServiceMock.Verify(x => x.CleanAllAsync(), Times.Never);
    }
}
