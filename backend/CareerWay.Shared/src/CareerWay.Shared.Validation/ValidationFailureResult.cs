namespace CareerWay.Shared.Validation.Validation;

public class ValidationFailureResult
{
    public string PropertyName { get; set; }

    public List<string?> ErrorMessages { get; set; }

    public ValidationFailureResult()
    {
        PropertyName = string.Empty;
        ErrorMessages = new List<string?>();
    }

    public ValidationFailureResult(string propertyName, List<string?> errorMessages)
    {
        PropertyName = propertyName;
        ErrorMessages = errorMessages;
    }
}
