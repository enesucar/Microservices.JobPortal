namespace CareerWay.Shared.Guids;

public class CustomGuidGenerator : IGuidGenerator
{
    public Guid Generate()
    {
        return Guid.NewGuid();
    }
}
