namespace PersonCatalog.Application.PersonDirectory.Commands.CreatePerson;

public class CreatePersonHandler(IPersonWriteRepository personWriteRepository, ICacheService cacheService, ILogger<CreatePersonHandler> logger)
    : ICommandHandler<CreatePersonCommand, CreatePersonResult>

{
    public async Task<CreatePersonResult> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var person = CreateNewPerson(command.Person);
        personWriteRepository.AddAsync(person,cancellationToken);
        cacheService.CleanAllAsync();

        logger.LogInformation($"Person created: {command.Person.FullName} with ID: {command.Person.Id}");
        return new CreatePersonResult(person.Id.Value);
    }

    private Person CreateNewPerson(PersonDto personDto) 
    {
        return Person.Create
            (
              id: PersonId.Of(Guid.NewGuid()),
              fullName: personDto.FullName,
              dateOfBirth: personDto.DateOfBirth,
              email : personDto.Email,
              phoneNumber: personDto.PhoneNumber,
              address : personDto.Address,
              genderStatus : personDto.Gender,
              nationality: personDto.Nationality,
              occupation : personDto.Occupation
            );
    }
}
