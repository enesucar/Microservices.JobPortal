using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Domain.Entities;

namespace CareerWay.JobAdvertService.Domain.Entities;

public class Question : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public string Title { get; set; } = default!;

    public QuestionType QuestionType { get; set; }

    public bool IsRequired { get; set; }
}
