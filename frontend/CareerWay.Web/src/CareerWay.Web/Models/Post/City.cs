namespace CareerWay.Web.Models.Post;

public class City
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}

public class Cities
{
    public IEnumerable<Position> Items { get; set; } = [];
}