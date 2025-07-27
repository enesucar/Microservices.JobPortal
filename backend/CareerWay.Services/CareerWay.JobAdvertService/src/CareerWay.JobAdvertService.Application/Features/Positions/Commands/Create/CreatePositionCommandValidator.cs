using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Positions.Commands.Create;

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    public CreatePositionCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(PositionConsts.MaxNameLength).NotEmpty();
    }
}
