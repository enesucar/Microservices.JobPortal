namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static T Null<T>(this IGuardClause guardClause, T? input, string parameterName)
    {
        if (input == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return input;
    }
}
