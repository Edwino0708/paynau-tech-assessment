namespace PersonCatalog.Application.Extensions;

public static class PersonExtension
{
    public static IEnumerable<PersonDto> ToPersonDtoList(this IEnumerable<Person> persons)
    {
        return persons.Select(person => new PersonDto(
               Id : person.Id.Value,
               FullName: person.FullName,
               DateOfBirth: person.DateOfBirth,
               Email: person.Email,
               PhoneNumber: person.PhoneNumber,
               Address: person.Address,
               Gender: person.Gender,
               Nationality: person.Nationality,
               Occupation: person.Occupation
            ));
    }
    public static PersonDto ToPersonDto(this Person person)
    {
        return new PersonDto(
            Id: person.Id.Value,
            FullName: person.FullName,
            DateOfBirth: person.DateOfBirth,
            Email: person.Email,
            PhoneNumber: person.PhoneNumber,
            Address: person.Address,
            Gender: person.Gender,
            Nationality: person.Nationality,
            Occupation: person.Occupation
            );
    }
}
