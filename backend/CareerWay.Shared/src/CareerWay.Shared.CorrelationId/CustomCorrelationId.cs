
namespace CareerWay.Shared.CorrelationId;

public class CustomCorrelationId : ICorrelationId
{
    private Guid _correlationId;

    public Guid Get()
    {
        return _correlationId;
    }

    public void Set(Guid correlationId)
    {
        _correlationId = correlationId;
    }
}
