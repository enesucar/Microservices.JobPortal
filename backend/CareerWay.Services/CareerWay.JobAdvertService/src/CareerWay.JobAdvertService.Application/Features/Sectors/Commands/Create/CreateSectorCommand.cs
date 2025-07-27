using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Create;

public class CreateSectorCommand : IRequest<CreateSectorResponse>
{
    public string Name { get; set; } = default!;
}
