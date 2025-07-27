namespace CareerWay.Shared.Validation.Validation;

public class ValidationFailureException : Exception
{
    public List<ValidationFailureResult> Errors { get; private set; }

    public ValidationFailureException(
        string? message = null,
        Exception? innerException = null,
        List<ValidationFailureResult>? errors = null)
        : base(message, innerException)
    {
        Errors = errors ?? [];
    }
}
