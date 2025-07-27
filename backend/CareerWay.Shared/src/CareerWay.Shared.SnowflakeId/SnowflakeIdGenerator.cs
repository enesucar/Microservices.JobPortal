using IdGen;

namespace CareerWay.Shared.SnowflakeId;

public class SnowflakeIdGenerator : ISnowflakeIdGenerator
{
    private readonly IdGenerator _idGenerator;

    public SnowflakeIdGenerator(IdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public long Generate()
    {
        return _idGenerator.CreateId();
    }
}
