using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Commands.Edit;

public class EditPositionCommand : IRequest<EditPositionResponse>
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
