using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Create;

public class CreateSectorCommandValidator : AbstractValidator<CreateSectorCommand>
{
    public CreateSectorCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(SectorConsts.MaxNameLength).NotEmpty();
    }
}
