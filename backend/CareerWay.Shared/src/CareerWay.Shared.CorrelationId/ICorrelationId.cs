namespace CareerWay.Shared.CorrelationId;

public interface ICorrelationId
{
    Guid Get();

    void Set(Guid correlationId);
}
