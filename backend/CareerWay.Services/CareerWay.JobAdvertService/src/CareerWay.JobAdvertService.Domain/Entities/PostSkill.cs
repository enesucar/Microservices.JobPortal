using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class PostSkill : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public long SkillId { get; set; }
}
