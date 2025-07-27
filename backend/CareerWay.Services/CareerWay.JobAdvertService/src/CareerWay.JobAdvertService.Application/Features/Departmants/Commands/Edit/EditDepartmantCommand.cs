using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Edit;

public class EditDepartmantCommand : IRequest<EditDepartmantResponse>
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;
}
