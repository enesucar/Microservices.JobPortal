using CareerWay.PaymentService.API;
using CareerWay.Shared.AspNetCore.Logging.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices(builder.Configuration, builder.Host);

var app = builder.Build();

app.EnableRequestBuffering();

app.UseRequestTime();

app.UseCorrelationId();

app.UseCustomHttpLogging();

app.UseHttpsRedirection();

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "PaymentService Open API";
    options.OperationSorter = OperationSorter.Method;
});

//app.PushSerilogProperties();

app.UseAuthorization();

app.MapControllers();

app.Run();
