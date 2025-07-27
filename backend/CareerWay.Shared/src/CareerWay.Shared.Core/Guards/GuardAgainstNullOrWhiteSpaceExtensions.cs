namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static string NullOrWhiteSpace(this IGuardClause guardClause, string? input, string parameterName)
    {
        guardClause.NullOrEmpty(input, parameterName);

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
        }

        return input;
    }

    public static string NullOrWhiteSpace(this IGuardClause guardClause, string? input, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        guardClause.NullOrEmpty(input, parameterName);

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"Required input {parameterName} was empty.", parameterName);
        }

        guardClause.Length(input, parameterName, maxLength, minLength);

        return input;
    }
}
