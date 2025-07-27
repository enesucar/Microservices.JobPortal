using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class PostMilitaryStatus : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public MilitaryStatus MilitaryStatus { get; set; }
}
