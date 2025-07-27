using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Sectors.Commands.Edit;

public class EditSectorCommandValidator : AbstractValidator<EditSectorCommand>
{
    public EditSectorCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(SectorConsts.MaxNameLength).NotEmpty();
    }
}
