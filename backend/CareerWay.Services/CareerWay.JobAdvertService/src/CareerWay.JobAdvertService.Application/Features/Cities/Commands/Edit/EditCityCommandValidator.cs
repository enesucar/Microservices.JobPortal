using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Commands.Edit;

public class EditCityCommandValidator : AbstractValidator<EditCityCommand>
{
    public EditCityCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(CityConsts.MaxNameLength).NotEmpty();
    }
}
