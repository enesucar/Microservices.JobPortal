using CareerWay.JobSeekerService.API.Services;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Events;
using CareerWay.JobSeekerService.Application.IntegrationEvents.Handlers;
using CareerWay.Shared.AspNetCore.Logging.Extensions;
using CareerWay.Shared.EventBus.Abstractions;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration, builder.Host);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();

await eventBus.SubscribeAsync<JobSeekerCreatedIntegrationEvent, JobSeekerCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PositionCreatedIntegrationEvent, PositionCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PositionEditedIntegrationEvent, PositionEditedIntegrationEventHandler>();
await eventBus.SubscribeAsync<SkillCreatedIntegrationEvent, SkillCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<SkillEditedIntegrationEvent, SkillEditedIntegrationEventHandler>();
 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.MapGrpcService<JobSeekerGrpcServiceImplementation>();

app.EnableRequestBuffering();

app.UseRequestTime();

app.UseCorrelationId();

app.UseHttpsRedirection();

app.MapOpenApi();

app.UseCors();

app.MapScalarApiReference(options =>
{
    options.Title = "JobSeekerService Open API";
    options.OperationSorter = OperationSorter.Method;
});

//app.PushSerilogProperties();

app.UseCustomHttpLogging();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler(o => { });

app.Run();
