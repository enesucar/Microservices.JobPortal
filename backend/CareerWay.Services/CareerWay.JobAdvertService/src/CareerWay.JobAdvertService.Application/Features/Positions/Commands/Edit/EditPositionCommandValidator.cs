using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Commands.Edit;

public class EditPositionCommandValidator : AbstractValidator<EditPositionCommand>
{
    public EditPositionCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(PositionConsts.MaxNameLength).NotEmpty();
    }
}
