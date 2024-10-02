namespace PersonCatalog.Application.Person.Commands.CreatePerson;

public record CreatePersonCommand(PersonDto Person);

public record CreatePersonResult(Guid Id);

public class CreatePersonCommandValidator
    : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(x => x.Person.FullName).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.DateOfBirth).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.Email).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.PhoneNumber).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.Address).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.Gender).NotNull().WithMessage(propety => $"{propety} is required");
    }
}