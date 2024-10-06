namespace Application.Tests.BDD.StepDefinitions;


[Binding]
public class UpdatePersonStepDefinitions
{
    private readonly Mock<IPersonReadRepository> _readRepositoryMock;
    private readonly Mock<IPersonWriteRepository> _writeRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly Mock<ILogger<UpdatePersonHandler>> _loggerMock;
    private readonly UpdatePersonHandler _handler;
    private UpdatePersonResult _result;
    private Exception _exception;

    public UpdatePersonStepDefinitions()
    {
        _readRepositoryMock = new Mock<IPersonReadRepository>();
        _writeRepositoryMock = new Mock<IPersonWriteRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _loggerMock = new Mock<ILogger<UpdatePersonHandler>>();
        _handler = new UpdatePersonHandler(_readRepositoryMock.Object, _writeRepositoryMock.Object, _cacheServiceMock.Object, _loggerMock.Object);
    }

    [Given(@"a repository with a person with update id ""(.*)""")]
    public void GivenARepositoryWithAPersonWithUpdateId(string id)
    {
        var personId = Guid.Parse(id);
        var person = Person.Create(
            PersonId.Of(personId),
            "John Doe",
            new DateTime(1990, 1, 1),
            "john.doe@example.com",
            "1234567890",
            "123 Main St",
            "Male",
            "American",
            "Engineer"
        );

        _readRepositoryMock.Setup(x => x.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);
        _readRepositoryMock.Setup(x => x.ExistsAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    }

    [Given(@"a repository without a person with update id ""(.*)""")]
    public void GivenARepositoryWithoutAPersonWithUpdateId(string id)
    {
        var personId = Guid.Parse(id);
        _readRepositoryMock.Setup(x => x.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null);
        _readRepositoryMock.Setup(x => x.ExistsAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
    }

    [When(@"I update the person with id ""(.*)""")]
    public async Task WhenIUpdateThePersonWithId(string id)
    {
        var personId = Guid.Parse(id);
        var command = new UpdatePersonCommand
        (
            new PersonDto
            (
                Id: personId,
                FullName: "John Doe",
                DateOfBirth: new DateTime(1990, 1, 1),
                Email: "john.doe@example.com",
                PhoneNumber: "1234567890",
                Address: "123 Main St",
                Gender: "Male",
                Nationality: "American",
                Occupation: "Engineer"
            )
        );

        try
        {
            _result = await _handler.Handle(command, CancellationToken.None);
        }
        catch (Exception ex)
        {
            _exception = ex;
        }
    }

    [Then(@"I should receive a successful update result")]
    public void ThenIShouldReceiveASuccessfulUpdateResult()
    {
        _result.Should().NotBeNull();
        _result.IsSuccess.Should().BeTrue();
    }

    [Then(@"I should receive a person not found exception")]
    public void ThenIShouldReceiveAPersonNotFoundException()
    {
        _exception.Should().BeOfType<PersonNotFoundException>();
    }
}