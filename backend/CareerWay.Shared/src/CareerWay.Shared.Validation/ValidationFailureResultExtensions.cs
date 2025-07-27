namespace CareerWay.Shared.Validation.Validation;

public static class ValidationFailureResultExtensions
{
    public static IDictionary<string, string?[]> ToDictionary(this List<ValidationFailureResult> result)
    {
        return result.ToDictionary(failure => failure.PropertyName, failure => failure.ErrorMessages.ToArray());
    }
}
