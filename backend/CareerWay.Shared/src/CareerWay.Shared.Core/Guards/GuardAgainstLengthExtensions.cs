namespace CareerWay.Shared.Core.Guards;

public static partial class GuardClauseExtensions
{
    public static string? Length(this IGuardClause guardClause, string? input, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        guardClause.Negative(minLength, nameof(minLength));
        guardClause.NegativeOrZero(maxLength, nameof(maxLength));

        if (minLength > maxLength)
        {
            throw new ArgumentException($"{nameof(minLength)} should be less or equal than {nameof(maxLength)}", parameterName);
        }

        if (minLength > 0)
        {
            guardClause.NullOrEmpty(input, parameterName);

            if (input?.Length < minLength)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be shorter than {minLength} character(s).");
            }
        }

        if (input != null && input.Length > maxLength)
        {
            throw new ArgumentException($"Required input {parameterName} cannot be longer than {maxLength} character(s).");
        }

        return input;
    }

    public static short Length(this IGuardClause guardClause, short input, string parameterName, short maxLength = short.MaxValue, short minLength = 0)
    {
        return Length<short>(guardClause, input, parameterName, maxLength, minLength);
    }

    public static int Length(this IGuardClause guardClause, int input, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
    {
        return Length<int>(guardClause, input, parameterName, maxLength, minLength);
    }

    private static T? Length<T>(this IGuardClause guardClause, T? input, string parameterName, T maxLength, T minLength)
        where T : IComparable
    {
        guardClause.Negative(minLength, nameof(minLength));
        guardClause.NegativeOrZero(maxLength, nameof(maxLength));

        if (minLength.CompareTo(maxLength) > 0)
        {
            throw new ArgumentException($"{nameof(minLength)} should be less or equal than {nameof(maxLength)}", parameterName);
        }

        if (minLength.CompareTo(default(T)) > 0)
        {
            guardClause.Null(input, parameterName);

            if (input?.CompareTo(minLength) < 0)
            {
                throw new ArgumentException($"Required input {parameterName} cannot be shorter than {minLength} character(s).");
            }
        }

        if (input != null && input.CompareTo(maxLength) > 0)
        {
            throw new ArgumentException($"Required input {parameterName} cannot be longer than {maxLength} character(s).");
        }

        return input;
    }
}
