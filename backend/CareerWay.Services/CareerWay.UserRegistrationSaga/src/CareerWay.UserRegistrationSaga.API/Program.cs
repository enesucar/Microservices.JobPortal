using CareerWay.Shared.AspNetCore.Logging.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices(builder.Configuration, builder.Host);

var app = builder.Build();

app.UseHttpsRedirection();

app.EnableRequestBuffering();

app.UseRequestTime();

app.UseCorrelationId();

app.UseHttpsRedirection();

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "UserRegistrationSaga Open API";
    options.OperationSorter = OperationSorter.Method;
});

app.PushSerilogProperties();

app.MapControllers();

app.UseCustomHttpLogging();

app.UseExceptionHandler(o => { });

app.Run();
