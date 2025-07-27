using CareerWay.SearchService.API.Consts;
using Nest;

namespace CareerWay.SearchService.API.Entities;

public class Position
{
    [PropertyName("id")]
    public long Id { get; set; }

    [PropertyName("name")]
    public string Name { get; set; } = default!;
}
