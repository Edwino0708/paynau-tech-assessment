namespace PersonCatalog.Application.PersonDirectory.Commands.DeletePerson;

public record DeletePersonCommand(Guid Id)
    :ICommand<DeletePersonResult>;

public record DeletePersonResult(bool IsSuccess);

public class DeletePersonValidator
    : AbstractValidator<DeletePersonCommand>
{
    public DeletePersonValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}
