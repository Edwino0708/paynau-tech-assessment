namespace PersonCatalog.Domain.Models;
public class Person : Aggregate<PersonId>
{
    public string FullName { get; private set; } = default!;
    public DateTime DateOfBirth { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public GenderStatus Gender { get; private set; } = default!;
    public string Nationality { get; private set; } = default!;
    public string Occupation { get; private set; } = default!;

    public static Person Create(PersonId id, string fullName, DateTime dateOfBirth, string email, string phoneNumber, string address, GenderStatus genderStatus, string nationality, string occupation)
    {
        var person = new Person
        {
            Id = id,
            FullName = fullName,
            DateOfBirth = dateOfBirth,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            Gender = genderStatus,
            Nationality = nationality,
            Occupation = occupation
        };

        person.AddDomainEvent(new PersonCreatedEvent(person));
        return person;
    }

    public void Update(string fullName, DateTime dateOfBirth, string email, string phoneNumber, string address, GenderStatus genderStatus, string nationality, string ocupation)
    {
        FullName = fullName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        Gender = genderStatus;
        Nationality = nationality;
        Occupation = ocupation;

        AddDomainEvent(new PersonUpdatedEvent(this));
    }
}
