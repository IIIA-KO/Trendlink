using Trendlink.Infrastructure.Extensions;

namespace Trendlink.Api.Extensions
{
    public static class DevelopmentExtensions
    {
        public static async Task ApplyDevelopmentSettings(
            this WebApplication app,
            IConfiguration configuration
        )
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.ApplyMigrations();
            await app.SeedDataAsync(configuration);
        }
    }
}
