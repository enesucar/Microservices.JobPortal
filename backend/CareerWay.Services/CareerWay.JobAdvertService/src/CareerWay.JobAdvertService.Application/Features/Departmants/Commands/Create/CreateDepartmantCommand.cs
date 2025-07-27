using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Departmants.Commands.Create;

public class CreateDepartmantCommand : IRequest<CreateDepartmantResponse>
{
    public string Name { get; set; } = default!;
}
