using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Cities.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Departmants.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.Payments.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.Payments.Events;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Positions.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.PostEducationLevels.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostLanguageRequirements.IntegrationEvents;
using CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Posts.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.PostSectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.IntegrationEvents.EventHandlers;
using CareerWay.JobAdvertService.Application.Features.PostWorkBenefits.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Sectors.IntegrationEvents.Handlers;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Events;
using CareerWay.JobAdvertService.Application.Features.Skills.IntegrationEvents.Handlers;
using CareerWay.Shared.AspNetCore.Logging.Extensions;
using CareerWay.Shared.EventBus.Abstractions;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration, builder.Host);

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

await eventBus.SubscribeAsync<CityCreatedIntegrationEvent, CityCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<CityEditedIntegrationEvent, CityEditedIntegrationEventHandler>();
await eventBus.SubscribeAsync<DepartmantCreatedIntegrationEvent, DepartmantCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<DepartmantEditedIntegrationEvent, DepartmantEditedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PositionCreatedIntegrationEvent, PositionCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PositionEditedIntegrationEvent, PositionEditedIntegrationEventHandler>();
await eventBus.SubscribeAsync<SectorCreatedIntegrationEvent, SectorCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<SectorEditedIntegrationEvent, SectorEditedIntegrationEventHandler>();
await eventBus.SubscribeAsync<SkillCreatedIntegrationEvent, SkillCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<SkillEditedIntegrationEvent, SkillEditedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostEducationLevelCreatedIntegrationEvent, PostEducationLevelCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostLanguageRequirementCreatedIntegrationEvent, PostLanguageRequirementCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostWorkBenefitCreatedIntegrationEvent, PostWorkBenefitCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostSectorCreatedIntegrationEvent, PostSectorCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostPublishedIntegrationEvent, PostPublishedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostCreatedIntegrationEvent, PostCreatedIntegrationEventHandler>();

app.EnableRequestBuffering();

app.UseRequestTime();

app.UseCorrelationId();

app.UseHttpsRedirection();

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "JobAdvertService Open API";
    options.OperationSorter = OperationSorter.Method;
});

//app.PushSerilogProperties();

app.UseCustomHttpLogging();

app.UseAuthorization();

app.MapControllers();

//app.UseExceptionHandler(o => { });

app.Run();
