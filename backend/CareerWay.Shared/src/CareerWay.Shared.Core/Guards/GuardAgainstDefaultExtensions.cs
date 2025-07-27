namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static T Default<T>(this IGuardClause guardClause, T input, string parameterName)
    {
        if (EqualityComparer<T>.Default.Equals(input, default))
        {
            throw new ArgumentException($"Parameter [{parameterName}] is default value for type {typeof(T).Name}", parameterName);
        }

        return input;
    }
}
