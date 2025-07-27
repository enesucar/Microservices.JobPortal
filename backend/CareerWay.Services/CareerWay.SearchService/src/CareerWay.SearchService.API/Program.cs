using CareerWay.SearchService.API.IntegrationEvents.EventHandlers;
using CareerWay.SearchService.API.IntegrationEvents.Events;
using CareerWay.Shared.EventBus.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices(builder.Configuration, builder.Host);

builder.Services.AddControllers();

var app = builder.Build();

app.UseCorrelationId();

app.EnableRequestBuffering();

app.UseRequestTime();

var eventBus = app.Services.GetRequiredService<IEventBus>();

await eventBus.SubscribeAsync<PostCreatedIntegrationEvent, PostCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<CompanyCreatedIntegrationEvent, CompanyCreatedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostPublishedIntegrationEvent, PostPublishedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostAppliedIntegrationEvent, PostAppliedIntegrationEventHandler>();
await eventBus.SubscribeAsync<PostWithdrawnIntegrationEvent, PostWithdrawnIntegrationEventHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
 

app.Run();
