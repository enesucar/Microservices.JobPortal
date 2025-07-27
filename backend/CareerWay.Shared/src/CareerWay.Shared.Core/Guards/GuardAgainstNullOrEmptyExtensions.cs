namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static string NullOrEmpty(this IGuardClause guardClause, string? input, string parameter)
    {
        guardClause.Null(input, nameof(input));

        if (input == string.Empty)
        {
            throw new ArgumentException($"Required input {parameter} was empty.", parameter);
        }

        return input!;
    }

    public static string NullOrEmpty(this IGuardClause guardClause, string? input, string parameter, int maxLength = int.MaxValue, int minLength = 0)
    {
        guardClause.Null(input, nameof(input));

        if (input == string.Empty)
        {
            throw new ArgumentException($"Required input {parameter} was empty.", parameter);
        }

        guardClause.Length(input, nameof(parameter), maxLength, minLength);

        return input!;
    }
}
