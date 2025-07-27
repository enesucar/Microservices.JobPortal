namespace CareerWay.Shared.Core.Exceptions;

public class BusinessException : Exception
{
    public object? Errors { get; set; }

    public string? Title { get; set; }

    public BusinessException(
        string? message = null,
        string? title = null,
        Exception? innerException = null,
        object? errors = null)
        : base(message, innerException)
    {
        Errors = errors;
        Title = title;
    }
}
