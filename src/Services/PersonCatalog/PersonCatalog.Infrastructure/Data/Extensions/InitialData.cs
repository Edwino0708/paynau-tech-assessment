namespace PersonCatalog.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Person> Persons = new List<Person>
    {
        Person.Create(
            PersonId.Of(Guid.NewGuid()), // Asumo que PersonId es un valor tipo GUID.
            "John Doe",
            new DateTime(1985, 5, 15),
            "johndoe@example.com",
            "555-1234",
            "123 Main St, Springfield",
            GenderStatus.MALE,
            "American",
            "Software Engineer"
        ),
        Person.Create(
            PersonId.Of(Guid.NewGuid()),
            "Jane Smith",
            new DateTime(1990, 7, 22),
            "janesmith@example.com",
            "555-5678",
            "456 Elm St, Metropolis",
            GenderStatus.FEMALE,
            "Canadian",
            "Doctor"
        ),
        Person.Create(
            PersonId.Of(Guid.NewGuid()),
            "Alice Johnson",
            new DateTime(1988, 3, 10),
            "alicej@example.com",
            "555-8765",
            "789 Oak St, Gotham",
            GenderStatus.FEMALE,
            "British",
            "Architect"
        ),
        Person.Create(
            PersonId.Of(Guid.NewGuid()),
            "Robert Brown",
            new DateTime(1975, 11, 30),
            "robertb@example.com",
            "555-4321",
            "321 Pine St, Star City",
            GenderStatus.MALE,
            "Australian",
            "Teacher"
        ),
        Person.Create(
            PersonId.Of(Guid.NewGuid()),
            "Maria Garcia",
            new DateTime(1995, 9, 5),
            "mariag@example.com",
            "555-9876",
            "654 Cedar St, Central City",
            GenderStatus.FEMALE,
            "Spanish",
            "Graphic Designer"
        )
    };

}
