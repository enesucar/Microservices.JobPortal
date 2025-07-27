using CareerWay.CompanyService.Application.Payments.EventHandlers;
using CareerWay.CompanyService.Application.Payments.Events;
using CareerWay.JobAdvertService.Application.Features.Payments.Events;
using CareerWay.Shared.AspNetCore.Logging.Extensions;
using CareerWay.Shared.EventBus.Abstractions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration, builder.Host);

var app = builder.Build();

var eventBus = app.Services.GetRequiredService<IEventBus>();
await eventBus.SubscribeAsync<PostCreatedIntegrationEvent, PostCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PaymentSuccessIntegrationEvent, PaymentSuccessIntegrationEventHandler>();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseCorrelationId();

app.EnableRequestBuffering();

app.UseRequestTime();

app.UseCorrelationId();

app.UseHttpsRedirection();

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "Company Open API";
    options.OperationSorter = OperationSorter.Method;
});

//app.PushSerilogProperties();

app.UseCustomHttpLogging();

app.MapControllers();

app.UseExceptionHandler(o => { });

app.Run();
