namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static short Positive(this IGuardClause guardClause, short input, string parameterName)
    {
        return Positive<short>(guardClause, input, parameterName);
    }

    public static int Positive(this IGuardClause guardClause, int input, string parameterName)
    {
        return Positive<int>(guardClause, input, parameterName);
    }

    public static long Positive(this IGuardClause guardClause, long input, string parameterName)
    {
        return Positive<long>(guardClause, input, parameterName);
    }

    public static decimal Positive(this IGuardClause guardClause, decimal input, string parameterName)
    {
        return Positive<decimal>(guardClause, input, parameterName);
    }

    public static float Positive(this IGuardClause guardClause, float input, string parameterName)
    {
        return Positive<float>(guardClause, input, parameterName);
    }

    public static double Positive(this IGuardClause guardClause, double input, string parameterName)
    {
        return Positive<double>(guardClause, input, parameterName);
    }

    public static TimeSpan Positive(this IGuardClause guardClause, TimeSpan input, string parameterName)
    {
        return Positive<TimeSpan>(guardClause, input, parameterName);
    }

    private static T Positive<T>(this IGuardClause guardClause, T input, string parameterName)
        where T : IComparable
    {
        if (input.CompareTo(default(T)) > 0)
        {
            throw new ArgumentException($"Required input {parameterName} cannot be positive.", parameterName);
        }

        return input;
    }
}
