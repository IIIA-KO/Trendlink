using Serilog;
using Trendlink.Api.Extensions;
using Trendlink.Application;
using Trendlink.Infrastructure;

namespace Trendlink.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog(
                (context, loggerConfig) =>
                    loggerConfig.ReadFrom.Configuration(context.Configuration)
            );

            builder.Services.AddControllers();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            WebApplication app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.ApplyMigrations();
                app.SeedData();
            }

            app.UseHttpsRedirection();

            app.UseRequestContextLogging();
            app.UseSerilogRequestLogging();

            app.UseCustomExceptionHandler();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
