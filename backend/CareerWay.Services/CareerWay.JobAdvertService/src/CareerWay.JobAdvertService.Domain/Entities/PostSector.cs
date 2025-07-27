using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class PostSector : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public long SectorId { get; set; }
}
