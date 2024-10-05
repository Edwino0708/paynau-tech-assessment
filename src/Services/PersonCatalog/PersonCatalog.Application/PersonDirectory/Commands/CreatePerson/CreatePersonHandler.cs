namespace PersonCatalog.Application.PersonDirectory.Commands.CreatePerson;

public class CreatePersonHandler(IApplicationDbContext dbContext, ICacheService cacheService)
    : ICommandHandler<CreatePersonCommand, CreatePersonResult>

{
    public async Task<CreatePersonResult> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
    {
        var person = CreateNewPerson(command.Person);

        dbContext.Persons.Add(person);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        cacheService.CleanAllAsync();

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
