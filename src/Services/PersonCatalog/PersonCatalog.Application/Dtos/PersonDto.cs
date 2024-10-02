namespace PersonCatalog.Application.Dtos;

public record PersonDto(
    string FullName,
    DateTime DateOfBirth,
    string Email,
    string PhoneNumber,
    string Address,
    GenderStatus Gender,
    string Nationality,
    string Occupation
    );
