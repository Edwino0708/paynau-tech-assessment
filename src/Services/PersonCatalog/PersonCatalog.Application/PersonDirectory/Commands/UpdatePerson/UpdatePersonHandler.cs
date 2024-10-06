
using PersonCatalog.Application.Services;

namespace PersonCatalog.Application.PersonDirectory.Commands.UpdatePerson;

public class UpdatePersonHandler(IPersonReadRepository readRepository, IPersonWriteRepository writeRepository, ICacheService cacheService, ILogger<UpdatePersonHandler> logger)
    : ICommandHandler<UpdatePersonCommand, UpdatePersonResult>
{
    public async Task<UpdatePersonResult> Handle(UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var personId = PersonId.Of(command.Person.Id);

        if (!await readRepository.ExistsAsync(personId, cancellationToken))
        {
            throw new PersonNotFoundException(command.Person.Id);
        }

        var person = await readRepository.GetByIdAsync(personId, cancellationToken);
        UpdatePersonWithNewValues(person, command.Person);
        await writeRepository.UpdateAsync(person, cancellationToken);
        await cacheService.CleanAllAsync();

        logger.LogInformation($"Person updated: {command.Person.FullName} with ID: {command.Person.Id}");
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

