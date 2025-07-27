using FluentValidation.Results;

namespace System;

public static class ValidationFailureExtensions
{
    public static IDictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> validationFailures)
    {
        return validationFailures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}
