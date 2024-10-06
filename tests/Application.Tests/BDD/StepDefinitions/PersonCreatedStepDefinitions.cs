using PersonCatalog.Application.PersonDirectory.Commands.CreatePerson;

namespace Application.Tests.BDD.StepDefinitions;

[Binding]
public class PersonCreatedStepDefinitions
{
    private readonly Mock<IPersonWriteRepository> _personWriteRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    private readonly Mock<ILogger<CreatePersonHandler>> _loggerMock;
    private CreatePersonHandler _handler;
    private PersonCreatedEvent _domainEvent;

    public PersonCreatedStepDefinitions()
    {
        _personWriteRepositoryMock = new Mock<IPersonWriteRepository>();
        _cacheServiceMock = new Mock<ICacheService>();
        _loggerMock = new Mock<ILogger<CreatePersonHandler>>();
        _handler = new CreatePersonHandler(_personWriteRepositoryMock.Object, _cacheServiceMock.Object, _loggerMock.Object);
    }

    [Given("a person created event occurs")]
    public void GivenAPersonCreatedEventOccurs()
    {
        var person = Person.Create(
            id: PersonId.Of(Guid.NewGuid()),
            fullName: "John Doe",
            dateOfBirth: DateTime.Now,
            email: "john@example.com",
            phoneNumber: "123456789",
            address: "Address",
            genderStatus: "Male",
            nationality: "Nationality",
            occupation: "Occupation"
        );

        _domainEvent = new PersonCreatedEvent(person);
    }

}