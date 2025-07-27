using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Commands.Create;

public class CreateSkillCommand : IRequest<CreateSkillResponse>
{
    public string Name { get; set; } = default!;
}
