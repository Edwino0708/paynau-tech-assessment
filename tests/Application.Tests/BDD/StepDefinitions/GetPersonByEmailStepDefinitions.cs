namespace Application.Tests.BDD.StepDefinitions;

[Binding]
public class GetPersonByEmailStepDefinitions
{
    private readonly Mock<IPersonReadRepository> _personReadRepositoryMock;
    private readonly Mock<ILogger<GetPersonByEmailHandler>> _loggerMock;
    private readonly GetPersonByEmailHandler _handler;
    private GetPersonByEmailQueryResult _result;

    public GetPersonByEmailStepDefinitions()
    {
        _personReadRepositoryMock = new Mock<IPersonReadRepository>();
        _loggerMock = new Mock<ILogger<GetPersonByEmailHandler>>();
        _handler = new GetPersonByEmailHandler(_personReadRepositoryMock.Object, _loggerMock.Object);
    }

    [Given(@"a repository with a person with email ""(.*)""")]
    public void GivenARepositoryWithAPersonWithEmail(string email)
    {
        var person = Person.Create(
            PersonId.Of(Guid.NewGuid()), // Use a new Guid here
            "John Doe",
            new DateTime(1990, 1, 1),
            email,
            "1234567890",
            "123 Main St",
            "Male",
            "American",
            "Engineer"
        );

        _personReadRepositoryMock.Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(person);
    }

    [Given(@"a repository without a person with email ""(.*)""")]
    public void GivenARepositoryWithoutAPersonWithEmail(string email)
    {
        _personReadRepositoryMock.Setup(x => x.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Person)null);
    }

    [When(@"I request a person by email ""(.*)""")]
    public async Task WhenIRequestAPersonByEmail(string email)
    {
        var query = new GetPersonByEmailQuery(email = email);
        _result = await _handler.Handle(query, CancellationToken.None);
    }

    [Then(@"I should receive the person information by email")]
    public void ThenIShouldReceiveThePersonInformationByEmail()
    {
        _result.Should().NotBeNull();
        _result.Person.Should().NotBeNull();
        _result.Person.Email.Should().Be("john.doe@example.com");
    }

    [Then(@"I should receive null person information by email")]
    public void ThenIShouldReceiveNullPersonInformationByEmail()
    {
        _result.Should().NotBeNull();
        _result.Person.Should().BeNull();
    }
}
