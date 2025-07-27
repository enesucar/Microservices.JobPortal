namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static short Negative(this IGuardClause guardClause, short input, string parameterName)
    {
        return Negative<short>(guardClause, input, parameterName);
    }

    public static int Negative(this IGuardClause guardClause, int input, string parameterName)
    {
        return Negative<int>(guardClause, input, parameterName);
    }

    public static long Negative(this IGuardClause guardClause, long input, string parameterName)
    {
        return Negative<long>(guardClause, input, parameterName);
    }

    public static decimal Negative(this IGuardClause guardClause, decimal input, string parameterName)
    {
        return Negative<decimal>(guardClause, input, parameterName);
    }

    public static float Negative(this IGuardClause guardClause, float input, string parameterName)
    {
        return Negative<float>(guardClause, input, parameterName);
    }

    public static double Negative(this IGuardClause guardClause, double input, string parameterName)
    {
        return Negative<double>(guardClause, input, parameterName);
    }

    public static TimeSpan Negative(this IGuardClause guardClause, TimeSpan input, string parameterName)
    {
        return Negative<TimeSpan>(guardClause, input, parameterName);
    }

    private static T Negative<T>(this IGuardClause guardClause, T input, string parameterName)
        where T : IComparable
    {
        if (input.CompareTo(default(T)) < 0)
        {
            throw new ArgumentException($"Required input {parameterName} cannot be negative", parameterName);
        }

        return input;
    }
}
