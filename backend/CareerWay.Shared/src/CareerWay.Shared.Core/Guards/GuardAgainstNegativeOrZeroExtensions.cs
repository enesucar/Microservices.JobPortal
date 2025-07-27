namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static short NegativeOrZero(this IGuardClause guardClause, short value, string parameter)
    {
        return NegativeOrZero<short>(guardClause, value, parameter);
    }

    public static int NegativeOrZero(this IGuardClause guardClause, int value, string parameter)
    {
        return NegativeOrZero<int>(guardClause, value, parameter);
    }

    public static long NegativeOrZero(this IGuardClause guardClause, long value, string parameter)
    {
        return NegativeOrZero<long>(guardClause, value, parameter);
    }

    public static decimal NegativeOrZero(this IGuardClause guardClause, decimal value, string parameter)
    {
        return NegativeOrZero<decimal>(guardClause, value, parameter);
    }

    public static float NegativeOrZero(this IGuardClause guardClause, float value, string parameter)
    {
        return NegativeOrZero<float>(guardClause, value, parameter);
    }

    public static double NegativeOrZero(this IGuardClause guardClause, double value, string parameter)
    {
        return NegativeOrZero<double>(guardClause, value, parameter);
    }

    public static TimeSpan NegativeOrZero(this IGuardClause guardClause, TimeSpan value, string parameter)
    {
        return NegativeOrZero<TimeSpan>(guardClause, value, parameter);
    }

    private static T NegativeOrZero<T>(this IGuardClause guardClause, T value, string parameter)
        where T : IComparable
    {
        if (value.CompareTo(default(T)) <= 0)
        {
            throw new ArgumentException($"Required input {parameter} cannot be negative of zero.", parameter);
        }

        return value;
    }
}
