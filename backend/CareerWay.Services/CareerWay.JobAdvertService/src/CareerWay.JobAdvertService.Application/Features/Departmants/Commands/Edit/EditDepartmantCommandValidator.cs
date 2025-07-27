using CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Edit;
using CareerWay.JobAdvertService.Domain.Consts;
using FluentValidation;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Create;

public class EditDepartmantCommandValidator : AbstractValidator<EditDepartmantCommand>
{
    public EditDepartmantCommandValidator()
    {
        RuleFor(o => o.Name).MaximumLength(DepartmantConsts.MaxNameLength).NotEmpty();
    }
}
