using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class PostEducationLevel : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public PostEducationLevelType EducationLevelType { get; set; }
}
