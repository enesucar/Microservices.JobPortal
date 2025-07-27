namespace CareerWay.Shared.Core.Guards;

public class Guard : IGuardClause
{
    public static Guard Against { get; } = new Guard();

    private Guard()
    {
    }
}
