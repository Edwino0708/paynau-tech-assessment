using FluentAssertions;
using PersonCatalog.Domain.Models;
using PersonCatalog.Domain.ValueObjects;
using PersonCatalog.Domain.Exceptions;
using PersonCatalog.Domain.Events;

namespace Domain.Tests.TDD;

public class PersonTests
{
    [Fact]
    public void CreatePerson_ShouldCreatePerson_WhenValidParameters()
    {
        // Arrange
        var id = PersonId.Of(Guid.NewGuid());
        var fullName = "John Doe";
        var dateOfBirth = new DateTime(1990, 1, 1);
        var email = "john.doe@example.com";
        var phoneNumber = "123456789";
        var address = "123 Main St";
        var genderStatus = GenderStatus.MALE;
        var nationality = "American";
        var occupation = "Developer";

        // Act
        var person = Person.Create(id, fullName, dateOfBirth, email, phoneNumber, address, genderStatus, nationality, occupation);

        // Assert
        person.FullName.Should().Be(fullName);
        person.DateOfBirth.Should().Be(dateOfBirth);
        person.Email.Should().Be(email);
        person.PhoneNumber.Should().Be(phoneNumber);
        person.Address.Should().Be(address);
        person.Gender.Should().Be(genderStatus);
        person.Nationality.Should().Be(nationality);
        person.Occupation.Should().Be(occupation);
        person.DomainEvents.Should().ContainSingle(e => e is PersonCreatedEvent);
    }

    [Fact]
    public void CreatePerson_ShouldThrowDomainException_WhenEmailIsInvalid()
    {
        // Arrange
        var id = PersonId.Of(Guid.NewGuid());
        var invalidEmail = "invalidEmailFormat";

        // Act
        Action act = () => Person.Create(id, "John Doe", new DateTime(1990, 1, 1), invalidEmail, "123456789", "123 Main St", GenderStatus.MALE, "American", "Developer");

        // Assert
        act.Should().Throw<DomainExcepetion>().WithMessage("Domain Excepetion \"Invalid email format\" throws from Domain Layer.");
    }

    [Fact]
    public void UpdatePerson_ShouldUpdatePersonDetails_WhenCalled()
    {
        // Arrange
        var id = PersonId.Of(Guid.NewGuid());
        var person = Person.Create(id, "John Doe", new DateTime(1990, 1, 1), "john.doe@example.com", "123456789", "123 Main St", GenderStatus.MALE, "American", "Developer");

        var updatedFullName = "John Smith";
        var updatedDateOfBirth = new DateTime(1992, 2, 2);
        var updatedEmail = "john.smith@example.com";
        var updatedPhoneNumber = "987654321";
        var updatedAddress = "456 Secondary St";
        var updatedGenderStatus = GenderStatus.FEMALE;
        var updatedNationality = "Canadian";
        var updatedOccupation = "Manager";

        // Act
        person.Update(updatedFullName, updatedDateOfBirth, updatedEmail, updatedPhoneNumber, updatedAddress, updatedGenderStatus, updatedNationality, updatedOccupation);

        // Assert
        person.FullName.Should().Be(updatedFullName);
        person.DateOfBirth.Should().Be(updatedDateOfBirth);
        person.Email.Should().Be(updatedEmail);
        person.PhoneNumber.Should().Be(updatedPhoneNumber);
        person.Address.Should().Be(updatedAddress);
        person.Gender.Should().Be(updatedGenderStatus);
        person.Nationality.Should().Be(updatedNationality);
        person.Occupation.Should().Be(updatedOccupation);
        person.DomainEvents.Should().ContainSingle(e => e is PersonUpdatedEvent);
    }

}
