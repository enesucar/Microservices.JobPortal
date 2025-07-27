namespace CareerWay.JobAdvertService.Domain.Entities;

public class QuestionOption
{
    public long Id { get; set; }

    public long QuestionId { get; set; }

    public string OptionText { get; set; } = default!;
}
