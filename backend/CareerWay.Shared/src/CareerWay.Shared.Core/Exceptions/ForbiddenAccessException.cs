namespace CareerWay.Shared.Core.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(
        string? message = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
