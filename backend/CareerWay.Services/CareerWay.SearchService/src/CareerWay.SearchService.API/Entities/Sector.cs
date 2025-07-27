using Nest;

namespace CareerWay.SearchService.API.Entities;

public class Sector
{
    [PropertyName("id")]
    public long Id { get; set; }

    [PropertyName("name")]
    public string Name { get; set; } = default!;
}
