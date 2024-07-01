using Trendlink.Domain.Users.Countries;
using Trendlink.Infrastructure;

namespace Trendlink.Api.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedData(
            this IApplicationBuilder app,
            IConfiguration configuration
        )
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!dbContext.Set<Country>().Any())
            {
                var countriesApiUrl = new Uri(configuration["Countries-API-URL"]!);

                List<Country> countries = await DataGenerator.GenerateCountriesWithStatesAsync(
                    countriesApiUrl
                );

                dbContext.Set<Country>().AddRange(countries);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
