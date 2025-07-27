namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static T DefaultOrNull<T>(this IGuardClause guardClause, T input, string parameterName)
    {
        guardClause.Null(input, parameterName);
        guardClause.Default(input, parameterName);

        return input;
    }
}
