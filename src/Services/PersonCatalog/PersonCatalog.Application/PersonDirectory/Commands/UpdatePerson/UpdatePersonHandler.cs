namespace PersonCatalog.Application.PersonDirectory.Commands.UpdatePerson;

public class UpdatePersonHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdatePersonCommand, UpdatePersonResult>
{
    public async Task<UpdatePersonResult> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var personId = PersonId.Of(command.Person.Id);
        var person = await dbContext.Persons.FindAsync([personId], cancellationToken: cancellationToken);

        if (person == null)
        {
            throw new PersonNotFoundException(command.Person.Id);
        }

        UpdatePersonWithNewValues(person, command.Person);

        dbContext.Persons.Update(person);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdatePersonResult(true);
    }

    public void UpdatePersonWithNewValues(Person person, PersonDto personDto)
    {       
        person.Update(
            fullName: personDto.FullName,
            dateOfBirth: personDto.DateOfBirth,
            email: personDto.Email,
            phoneNumber: personDto.PhoneNumber,
            address: personDto.Address,
            genderStatus: personDto.Gender,
            nationality: personDto.Nationality,
            ocupation: personDto.Occupation
            );
    }
}

