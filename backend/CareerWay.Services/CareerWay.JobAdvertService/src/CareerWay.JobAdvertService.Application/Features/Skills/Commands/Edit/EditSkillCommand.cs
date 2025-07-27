using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Commands.Edit;

public class EditSkillCommand : IRequest<EditSkillResponse>
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
