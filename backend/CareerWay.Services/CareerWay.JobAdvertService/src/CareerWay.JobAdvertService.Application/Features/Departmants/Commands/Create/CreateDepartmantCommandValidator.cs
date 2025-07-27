using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Create;

public class CreateDepartmantCommandValidator : AbstractValidator<CreateDepartmantCommand>
{
    public CreateDepartmantCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(DepartmantConsts.MaxNameLength).NotEmpty();
    }
}
