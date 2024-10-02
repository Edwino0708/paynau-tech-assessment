namespace BuildingBlock.Exceptions;

public class BadRequestsException : Exception
{
    public BadRequestsException(string message) : base(message)
    {
        
    }


    public BadRequestsException(string message, string details) : base(message)
    {
        Details = details;
    }

    public string? Details { get; }
}
