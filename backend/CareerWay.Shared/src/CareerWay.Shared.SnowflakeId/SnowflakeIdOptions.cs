using IdGen;

namespace CareerWay.Shared.SnowflakeId;

public class SnowflakeIdOptions
{
    public int GeneratorId { get; set; }

    public IdGeneratorOptions IdGeneratorOptions { get; set; } = new IdGeneratorOptions();
}
