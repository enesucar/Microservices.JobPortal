using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Commands.Create;

public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(CityConsts.MaxNameLength).NotEmpty();
    }
}
