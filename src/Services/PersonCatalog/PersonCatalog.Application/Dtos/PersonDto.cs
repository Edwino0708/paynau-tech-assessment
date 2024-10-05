namespace PersonCatalog.Application.Dtos;

public record PersonDto(
    Guid Id,
    string FullName,
    DateTime DateOfBirth,
    string Email,
    string PhoneNumber,
    string Address,
    string Gender,
    string Nationality,
    string Occupation
    );
