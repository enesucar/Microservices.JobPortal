using CareerWay.JobAdvertService.Domain.Enums;
using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostEducationLevels.Commands.Create;

public class CreatePostEducationLevelCommand : IRequest<Unit>
{
    public long PostId { get; set; }

    public List<CreatePostEducationLevelItemCommand> Items { get; set; } = [];
}

public class CreatePostEducationLevelItemCommand
{
    public PostEducationLevelType EducationLevelType { get; set; }
}
