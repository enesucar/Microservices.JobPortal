using AutoMapper;
using CareerWay.JobAdvertService.Application.Features.Positions.Commands.Create;
using CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Interfaces;
using CareerWay.JobAdvertService.Domain.Entities;
using CareerWay.JobAdvertService.Domain.Enums;
using CareerWay.Shared.Core.Exceptions;
using CareerWay.Shared.CorrelationId;
using CareerWay.Shared.EventBus.Abstractions;
using CareerWay.Shared.Security.Users;
using CareerWay.Shared.SnowflakeId;
using CareerWay.Shared.TimeProviders;
using MediatR;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;

namespace CareerWay.JobAdvertService.Application.Features.Posts.Commands.Create;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostResponse>
{
    private readonly IJobAdvertWriteDbContext _context;
    private readonly IMapper _mapper;
    private readonly ISnowflakeIdGenerator _snowflakeIdGenerator;
    private readonly IEventBus _eventBus;
    private readonly IDateTime _dateTime;
    private readonly ICorrelationId _correlationId;
    private readonly IUser _user;
    private readonly ISlugGenerator _slugGenerator;

    public CreatePostCommandHandler(
        IJobAdvertWriteDbContext context,
        IMapper mapper,
        ISnowflakeIdGenerator snowflakeIdGenerator,
        IEventBus eventBus,
        IDateTime dateTime,
        ICorrelationId correlationId,
        IUser user,
        ISlugGenerator slugGenerator)
    {
        _context = context;
        _mapper = mapper;
        _snowflakeIdGenerator = snowflakeIdGenerator;
        _eventBus = eventBus;
        _dateTime = dateTime;
        _correlationId = correlationId;
        _user = user;
        _slugGenerator = slugGenerator;
    }

    public async Task<CreatePostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = _mapper.Map<Post>(request);
        post.Id = _snowflakeIdGenerator.Generate();
        post.PackageId = request.PackageId;
        post.CreationDate = _dateTime.Now;
        post.CompanyId = _user.Id;
        post.Status = PostStatus.Draft;
        post.Slug = _slugGenerator.Generate(post.Title);
        post.CreationDate = _dateTime.Now;

        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        var departmant = _context.Departmants.FirstOrDefault(o => o.Id == request.DepartmantId);
        var position = _context.Positions.FirstOrDefault(o => o.Id == request.PositionId);
        var city = _context.Cities.FirstOrDefault(o => o.Id == request.CityId);

        var postCreatedIntegrationEvent = new PostCreatedIntegrationEvent(_correlationId.Get(), _dateTime.Now);
        _mapper.Map(post, postCreatedIntegrationEvent);
        postCreatedIntegrationEvent.CompanyPackageId = request.CompanyPackageId;
        postCreatedIntegrationEvent.CompanyId = post.CompanyId;
        postCreatedIntegrationEvent.DepartmantId = departmant.Id;
        postCreatedIntegrationEvent.DepartmantName = departmant.Name;
        postCreatedIntegrationEvent.PositionId = position.Id;
        postCreatedIntegrationEvent.PositionName = position.Name;
        postCreatedIntegrationEvent.CityId = city.Id;
        postCreatedIntegrationEvent.CityName = city.Name;
        await _eventBus.PublishAsync(postCreatedIntegrationEvent);

        return _mapper.Map<CreatePostResponse>(post);
    }
}
