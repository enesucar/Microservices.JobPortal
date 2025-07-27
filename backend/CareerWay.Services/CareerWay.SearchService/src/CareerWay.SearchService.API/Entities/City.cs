using CareerWay.SearchService.API.Consts;
using Nest;

namespace CareerWay.SearchService.API.Entities;

public class City
{
    [PropertyName("id")]
    public int Id { get; set; }

    [PropertyName("name")]
    public string Name { get; set; } = default!;
}
