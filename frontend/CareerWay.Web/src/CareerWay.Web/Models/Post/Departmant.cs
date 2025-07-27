namespace CareerWay.Web.Models.Post;
  
public class Departmant
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}

public class Departmants
{
    public IEnumerable<Departmant> Items { get; set; } = [];
}