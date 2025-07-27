namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static short PositiveOrZero(this IGuardClause guardClause, short input, string parameterName)
    {
        return PositiveOrZero<short>(guardClause, input, parameterName);
    }

    public static int PositiveOrZero(this IGuardClause guardClause, int input, string parameterName)
    {
        return PositiveOrZero<int>(guardClause, input, parameterName);
    }

    public static long PositiveOrZero(this IGuardClause guardClause, long input, string parameterName)
    {
        return PositiveOrZero<long>(guardClause, input, parameterName);
    }

    public static decimal PositiveOrZero(this IGuardClause guardClause, decimal input, string parameterName)
    {
        return PositiveOrZero<decimal>(guardClause, input, parameterName);
    }

    public static float PositiveOrZero(this IGuardClause guardClause, float input, string parameterName)
    {
        return PositiveOrZero<float>(guardClause, input, parameterName);
    }

    public static double PositiveOrZero(this IGuardClause guardClause, double input, string parameterName)
    {
        return PositiveOrZero<double>(guardClause, input, parameterName);
    }

    public static TimeSpan PositiveOrZero(this IGuardClause guardClause, TimeSpan input, string parameterName)
    {
        return PositiveOrZero<TimeSpan>(guardClause, input, parameterName);
    }

    private static T PositiveOrZero<T>(this IGuardClause guardClause, T input, string parameterName)
        where T : IComparable
    {
        if (input.CompareTo(default(T)) >= 0)
        {
            throw new ArgumentException($"Required input {parameterName} cannot be positive or zero.", parameterName);
        }

        return input;
    }
}
