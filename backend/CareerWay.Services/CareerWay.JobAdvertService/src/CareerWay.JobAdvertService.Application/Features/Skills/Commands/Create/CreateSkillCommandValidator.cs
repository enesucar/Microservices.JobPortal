using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Commands.Create;

public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(SkillConsts.MaxNameLength).NotEmpty();
    }
}
