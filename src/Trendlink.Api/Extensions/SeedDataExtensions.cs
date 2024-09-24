using MediatR;
using Microsoft.EntityFrameworkCore;
using Trendlink.Application.Users.Authentication.RegisterUser;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;
using Trendlink.Infrastructure;
using Condition = Trendlink.Domain.Conditions.Condition;
using Role = Trendlink.Domain.Users.Role;
using User = Trendlink.Domain.Users.User;

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

            if (!await dbContext.Set<Country>().AnyAsync())
            {
                var countriesApiUrl = new Uri(configuration["Countries-API-URL"]!);

                List<Country> countries = await DataGenerator.GenerateCountriesWithStatesAsync(
                    countriesApiUrl
                );

                dbContext.Set<Country>().AddRange(countries);

                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Set<User>().AnyAsync())
            {
                const string password = "Pa$$w0rd";

                (User admin, List<User> users) = DataGenerator.GenerateUsers();

                StateId stateId = (await dbContext.Set<State>().FirstAsync()).Id;

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

            if (!await dbContext.Set<Condition>().AnyAsync())
            {
                List<User> users = await dbContext.Set<User>().ToListAsync();
                List<Condition> conditions = DataGenerator.GenerateConditionsForUsers(users);

                dbContext.Set<Condition>().AddRange(conditions);
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Set<Advertisement>().AnyAsync())
            {
                foreach (Condition? condition in await dbContext.Set<Condition>().ToListAsync())
                {
                    List<Advertisement> advertisements =
                        DataGenerator.GenerateAdvertisementsForCondition(condition);

                    dbContext.Set<Advertisement>().AddRange(advertisements);
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
