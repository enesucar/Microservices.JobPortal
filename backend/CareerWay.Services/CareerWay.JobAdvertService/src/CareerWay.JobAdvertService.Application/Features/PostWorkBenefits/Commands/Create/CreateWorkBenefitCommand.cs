using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.Commands.Create;

public class CreateWorkBenefitCommand : IRequest<Unit>
{
    public long PostId { get; set; }

    public List<CreateWorkBenefitItemCommand> Items { get; set; } = [];
}

public class CreateWorkBenefitItemCommand
{
    public string Name { get; set; } = default!;
}
