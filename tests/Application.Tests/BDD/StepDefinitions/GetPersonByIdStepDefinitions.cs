namespace Application.Tests.BDD.StepDefinitions;

[Binding]
public class GetPersonByIdStepDefinitions
{
    private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
    private readonly Mock<ILogger<GetPersonByIdHandler>> _loggerMock;
    private readonly GetPersonByIdHandler _handler;
    private GetPersonByIdQueryResult _result;

    public GetPersonByIdStepDefinitions()
    {
        _personReadRepositoryMock = new Mock<IPersonReadRepository>();
        _loggerMock = new Mock<ILogger<GetPersonByIdHandler>>();
        _handler = new GetPersonByIdHandler(_personReadRepositoryMock.Object, _loggerMock.Object);
    }

    [Given(@"a repository with a person with id ""(.*)""")]
    public void GivenARepositoryWithAPersonWithId(string id)
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

        _personReadRepositoryMock.Setup(x => x.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);
    }

    [Given(@"a repository without a person with id ""(.*)""")]
    public void GivenARepositoryWithoutAPersonWithId(string id)
    {
        var personId = Guid.Parse(id);
        _personReadRepositoryMock.Setup(x => x.GetByIdAsync(PersonId.Of(personId), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null);
    }

    [When(@"I request a person by id ""(.*)""")]
    public async Task WhenIRequestAPersonById(string id)
    {
        var personId = Guid.Parse(id);
        var query = new GetPersonByIdQuery(personId);
        _result = await _handler.Handle(query, CancellationToken.None);
    }

    [Then(@"I should receive the person information by id")]
    public void ThenIShouldReceiveThePersonInformationById()
    {
        _result.Should().NotBeNull();
        _result.Person.Should().NotBeNull();
        _result.Person.FullName.Should().Be("John Doe");
    }

    [Then(@"I should receive null person information by id")]
    public void ThenIShouldReceiveNullPersonInformationById()
    {
        _result.Should().NotBeNull();
        _result.Person.Should().BeNull();
    }
}