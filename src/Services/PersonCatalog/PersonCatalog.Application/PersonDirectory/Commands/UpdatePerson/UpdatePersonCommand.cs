namespace PersonCatalog.Application.PersonDirectory.Commands.UpdatePerson;

public record UpdatePersonCommand(PersonDto Person)
    :ICommand<UpdatePersonResult>;

public record UpdatePersonResult(bool IsSuccess);

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand> 
{
    public UpdatePersonCommandValidator()
    {
        RuleFor(x => x.Person.Id).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.FullName).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.DateOfBirth).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.Email).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.PhoneNumber).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.Address).NotNull().WithMessage(propety => $"{propety} is required");
        RuleFor(x => x.Person.Gender).NotNull().WithMessage(propety => $"{propety} is required");
    }
}
