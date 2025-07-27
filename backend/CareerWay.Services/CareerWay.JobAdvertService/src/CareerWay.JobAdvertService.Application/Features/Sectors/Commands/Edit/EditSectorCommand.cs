using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Edit;

public class EditSectorCommand : IRequest<EditSectorResponse>
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
