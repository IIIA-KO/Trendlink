using Trendlink.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilogLogging();

builder.Services.ConfigureServices(builder.Configuration);

WebApplication app = builder.Build();

app.ConfigureMiddleware();

if (app.Environment.IsDevelopment())
{
    await app.ApplyDevelopmentSettings(builder.Configuration);
}

await app.RunAsync();

public partial class Program;
