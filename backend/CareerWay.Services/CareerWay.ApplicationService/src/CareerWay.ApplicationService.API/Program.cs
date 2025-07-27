using CareerWay.ApplicationService.API;
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
    options.Title = "ApplicationService Open API";
    options.OperationSorter = OperationSorter.Method;
});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

//app.UsePushSerilogProperties();

app.UseAuthorization();

app.MapControllers();

//app.UseExceptionHandler(o => { });

app.Run();
