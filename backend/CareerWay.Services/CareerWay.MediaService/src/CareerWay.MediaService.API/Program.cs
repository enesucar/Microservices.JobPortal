using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebServices(builder.Configuration, builder.Host);

var app = builder.Build();

app.EnableRequestBuffering();

app.UseRequestTime();

app.UseCorrelationId();

//app.UseCustomHttpLogging();

app.UseHttpsRedirection();

app.MapOpenApi();

app.MapScalarApiReference(options =>
{
    options.Title = "MediaService Open API";
    options.OperationSorter = OperationSorter.Method;
});
   
app.UseAuthorization();

app.MapControllers(); 

//app.UsePushSerilogProperties();

//app.UseExceptionHandler(o => { });

app.Run();
