using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using PersonCatalog.Application.Dtos;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PersonCatalog.API.EndPoints;

namespace Api.Tests.TDD;
public class CreatePersonTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CreatePersonTests(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePerson_ReturnsCreated_WhenDataIsValid()
    {
        var request = new CreatePersonRequest(new PersonDto
        (
            Id: Guid.NewGuid(),
            FullName: "John Doe",
            DateOfBirth: new DateTime(1990, 1, 1),
            Email: "john.doe@example.com",
            PhoneNumber: "1234567890",
            Address: "123 Main St",
            Gender: "Male",
            Nationality: "American",
            Occupation: "Engineer"
        ));

        var response = await _client.PostAsJsonAsync("/persons", request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreatePerson_ReturnsBadRequest_WhenDataIsInvalid()
    {
        var request = new CreatePersonRequest(new PersonDto
        (
            Id: Guid.NewGuid(),
            FullName: "",
            DateOfBirth: new DateTime(1990, 1, 1),
            Email: "invalid-email",
            PhoneNumber: "1234567890",
            Address: "123 Main St",
            Gender: "Male",
            Nationality: "American",
            Occupation: "Engineer"
        ));

        var response = await _client.PostAsJsonAsync("/persons", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
