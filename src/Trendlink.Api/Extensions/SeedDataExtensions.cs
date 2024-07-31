using MediatR;
using Trendlink.Application.Users.RegisterUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;
using Trendlink.Infrastructure;

namespace Trendlink.Api.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedDataAsync(
            this IApplicationBuilder app,
            IConfiguration configuration
        )
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            ISender sender = scope.ServiceProvider.GetRequiredService<ISender>();

            if (!dbContext.Set<Country>().Any())
            {
                var countriesApiUrl = new Uri(configuration["Countries-API-URL"]!);

                List<Country> countries = await DataGenerator.GenerateCountriesWithStatesAsync(
                    countriesApiUrl
                );

                dbContext.Set<Country>().AddRange(countries);

                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Set<User>().Any())
            {
                const string password = "Pa$$w0rd";

                (User admin, List<User> users) = DataGenerator.GenerateUsers();

                StateId stateId = dbContext.Set<State>().First().Id;

                RegisterUserCommand adminCommand =
                    new(
                        admin.FirstName,
                        admin.LastName,
                        admin.BirthDate,
                        admin.Email,
                        admin.PhoneNumber,
                        password,
                        stateId
                    );

                Result<UserId> result = await sender.Send(adminCommand, default);

                if (result.IsSuccess)
                {
                    User? administrator = await dbContext.Set<User>().FindAsync(result.Value);

                    if (administrator is not null)
                    {
                        administrator.AddRole(Role.Administrator);
                        dbContext.Set<User>().Update(administrator);
                    }
                }

                foreach (User user in users)
                {
                    var userRegisterCommand = new RegisterUserCommand(
                        user.FirstName,
                        user.LastName,
                        user.BirthDate,
                        user.Email,
                        user.PhoneNumber,
                        password,
                        stateId
                    );

                    await sender.Send(userRegisterCommand, default);
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
