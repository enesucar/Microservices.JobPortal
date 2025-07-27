using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Commands.Create;

public class CreatePositionCommand : IRequest<CreatePositionResponse>
{
    public string Name { get; set; } = default!;
}
