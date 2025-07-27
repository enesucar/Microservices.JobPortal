using CareerWay.JobAdvertService.Domain.Enums;
using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.Commands;

public class CreatePostLanguageRequirementCommand : IRequest<Unit>
{
    public long PostId { get; set; }

    public List<CreatePostLanguageRequirementItemCommand> Items { get; set; } = [];
}

public class CreatePostLanguageRequirementItemCommand
{
    public LanguageType LanguageType { get; set; }

    public ReadingLevelType ReadingLevelType { get; set; }

    public WritingLevelType WritingLevelType { get; set; }

    public SpeakingLevelType SpeakingLevelType { get; set; }

}

