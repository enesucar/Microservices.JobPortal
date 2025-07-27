using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostSectors.Commands.Create;

public class CreatePostSectorCommand : IRequest<Unit>
{
    public long PostId { get; set; }

    public List<CreatePostSectorItemCommand> Items { get; set; } = [];
}

public class CreatePostSectorItemCommand
{
    public long SectorId { get; set; }
}
