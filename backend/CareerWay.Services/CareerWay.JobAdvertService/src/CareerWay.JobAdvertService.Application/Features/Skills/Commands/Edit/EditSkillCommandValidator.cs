using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Skills.Commands.Edit;

public class EditSkillCommandValidator : AbstractValidator<EditSkillCommand>
{
    public EditSkillCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(SkillConsts.MaxNameLength).NotEmpty();
    }
}
