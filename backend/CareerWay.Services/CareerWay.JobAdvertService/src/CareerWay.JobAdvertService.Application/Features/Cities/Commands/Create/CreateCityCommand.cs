using MediatR;

namespace CareerWay.JobAdvertService.Application.Features.Cities.Commands.Create;

public class CreateCityCommand : IRequest<CreateCityResponse>
{
    public string Name { get; set; } = default!;
}
