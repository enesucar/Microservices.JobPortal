using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Commands.Edit;

public class EditCityCommand : IRequest<EditCityResponse>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}
