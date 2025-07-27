using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobSeekerService.Domain.Entities;

public class JobSeekerSkill : Entity
{
    public Guid Id { get; set; }

    public long JobSeekerId { get; set; }

    public long SkillId { get; set; }

    public JobSeeker JobSeeker { get; set; } = default!;

    public Skill Skill { get; set; } = default!;
}
