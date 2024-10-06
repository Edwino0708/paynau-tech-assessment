namespace PersonCatalog.Domain.Models;
public class Person : Aggregate<PersonId>
{
    public string FullName { get; private set; } = default!;
    public DateTime DateOfBirth { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string Gender { get; private set; } = default!;
    public string Nationality { get; private set; } = default!;
    public string Occupation { get; private set; } = default!;

    public static Person Create(PersonId id, string fullName, DateTime dateOfBirth, string email, string phoneNumber, string address, string genderStatus, string nationality, string occupation)
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

        if (!IsValidEmail(email))
        {
            throw new DomainExcepetion("Invalid email format");
        }

        person.AddDomainEvent(new PersonCreatedEvent(person));
        return person;
    }

    public void Update(string fullName, DateTime dateOfBirth, string email, string phoneNumber, string address, string genderStatus, string nationality, string ocupation)
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

    private static bool IsValidEmail(string email)
    {
        return email.Contains("@") && email.IndexOf('@') < email.Length - 1;
    }

}
