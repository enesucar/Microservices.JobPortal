namespace CareerWay.Web.Models.Post;
 
public class Position
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}

public class Positions
{
    public IEnumerable<Position> Items { get; set; } = [];
}