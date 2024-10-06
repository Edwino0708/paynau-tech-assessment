using TechTalk.SpecFlow;
using FluentAssertions;
using PersonCatalog.Domain.Models;
using PersonCatalog.Domain.ValueObjects;

namespace Domain.Tests.BDD;

[Binding]
public class PersonSteps
{
    private Person _person;
    private PersonId _id;
    private string _fullName;
    private DateTime _dateOfBirth;
    private string _email;
    private string _phoneNumber;
    private string _address;
    private string _genderStatus;
    private string _nationality;
    private string _occupation;

    [Given(@"I have a valid person data")]
    public void GivenIHaveAValidPersonData()
    {
        _id = PersonId.Of(Guid.NewGuid());
        _fullName = "John Doe";
        _dateOfBirth = new DateTime(1990, 1, 1);
        _email = "john.doe@example.com";
        _phoneNumber = "123456789";
        _address = "123 Main St";
        _genderStatus = GenderStatus.MALE;
        _nationality = "American";
        _occupation = "Developer";
    }

    [When(@"I create a person")]
    public void WhenICreateAPerson()
    {
        _person = Person.Create(_id, _fullName, _dateOfBirth, _email, _phoneNumber, _address, _genderStatus, _nationality, _occupation);
    }

    [Then(@"the person should be created with the correct details")]
    public void ThenThePersonShouldBeCreatedWithTheCorrectDetails()
    {
        _person.FullName.Should().Be(_fullName);
        _person.DateOfBirth.Should().Be(_dateOfBirth);
        _person.Email.Should().Be(_email);
        _person.PhoneNumber.Should().Be(_phoneNumber);
        _person.Address.Should().Be(_address);
        _person.Gender.Should().Be(_genderStatus);
        _person.Nationality.Should().Be(_nationality);
        _person.Occupation.Should().Be(_occupation);
    }
}