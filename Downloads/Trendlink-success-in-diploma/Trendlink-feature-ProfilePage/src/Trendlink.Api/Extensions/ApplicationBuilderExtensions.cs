using Microsoft.EntityFrameworkCore;
using Trendlink.Api.Middleware;
using Trendlink.Infrastructure;

namespace Trendlink.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            try
            {
                dbContext.Database.Migrate();
            }
            catch (Npgsql.PostgresException)
            {
                Thread.Sleep(10000);
                dbContext.Database.Migrate();
            }
        }

        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestContextLoggingMiddleware>();
            return app;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy(
                    "CorsPolicy",
                    policy =>
                        policy
                            .WithOrigins("https://localhost:3000", "http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                )
            );

            return services;
        }
    }
}
