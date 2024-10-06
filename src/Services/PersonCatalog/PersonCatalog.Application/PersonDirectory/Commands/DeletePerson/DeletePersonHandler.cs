
namespace PersonCatalog.Application.PersonDirectory.Commands.DeletePerson;

public class DeletePersonHandler(IPersonWriteRepository personWriterRepository,IPersonReadRepository personReadRepository, ICacheService cacheService, ILogger<DeletePersonHandler> logger)
    : ICommandHandler<DeletePersonCommand, DeletePersonResult>
{
    public async Task<DeletePersonResult> Handle(DeletePersonCommand command, CancellationToken cancellationToken)
    {   
        var personId = PersonId.Of(command.Id);

        // Verifica si la persona existe antes de intentar eliminarla
        if (!await personReadRepository.ExistsAsync(personId, cancellationToken))
        {
            throw new PersonNotFoundException(command.Id);
        }

        await personWriterRepository.DeleteAsync(personId, cancellationToken);
        await cacheService.CleanAllAsync();

        logger.LogInformation($"Person deleted with ID: {command.Id}");
        return new DeletePersonResult(true);
    
    }
}
