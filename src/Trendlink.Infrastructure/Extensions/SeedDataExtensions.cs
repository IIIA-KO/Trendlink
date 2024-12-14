using System.Security.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trendlink.Application.Accounts.Register;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;

namespace Trendlink.Infrastructure.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedDataAsync(
            this IApplicationBuilder app,
            IConfiguration configuration
        )
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            await using ApplicationDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            ISender sender = scope.ServiceProvider.GetRequiredService<ISender>();

            await SeedCountries(configuration, dbContext);

            await SeedUsers(dbContext, sender);

            await SeedConditions(dbContext);

            await SeedAdvertisements(dbContext);

            await SeedCooperations(dbContext);

            await SeedReviews(dbContext);
        }

        private static async Task SeedCountries(
            IConfiguration configuration,
            ApplicationDbContext dbContext
        )
        {
            if (!await dbContext.Set<Country>().AnyAsync())
            {
                var countriesApiUrl = new Uri(configuration["Countries-API-URL"]!);

                List<Country> countries = await DataGenerator.GenerateCountriesWithStatesAsync(
                    countriesApiUrl
                );

                dbContext.Set<Country>().AddRange(countries);

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedUsers(ApplicationDbContext dbContext, ISender sender)
        {
            if (!await dbContext.Set<User>().AnyAsync())
            {
                const string password = "Pa$$w0rd";

                (User admin, List<User> users) = DataGenerator.GenerateUsers();

                List<StateId> stateIds = await dbContext
                    .Set<State>()
                    .Select(s => s.Id)
                    .ToListAsync();

                RegisterCommand adminCommand =
                    new(
                        admin.FirstName,
                        admin.LastName,
                        admin.BirthDate,
                        admin.Email,
                        admin.PhoneNumber,
                        password,
                        stateIds[RandomNumberGenerator.GetInt32(stateIds.Count)]
                    );

                Result<UserId> adminResult = await sender.Send(adminCommand, default);

                if (adminResult.IsSuccess)
                {
                    User? administrator = await dbContext.Set<User>().FindAsync(adminResult.Value);

                    if (administrator is not null)
                    {
                        administrator.AddRole(Role.Administrator);
                        dbContext.Set<User>().Update(administrator);
                    }
                }

                foreach (User user in users)
                {
                    StateId randomStateId = stateIds[
                        RandomNumberGenerator.GetInt32(stateIds.Count)
                    ];

                    var userRegisterCommand = new RegisterCommand(
                        user.FirstName,
                        user.LastName,
                        user.BirthDate,
                        user.Email,
                        user.PhoneNumber,
                        password,
                        randomStateId
                    );

                    await sender.Send(userRegisterCommand, default);
                }

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedConditions(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Set<Condition>().AnyAsync())
            {
                List<User> users = await dbContext.Set<User>().ToListAsync();
                List<Condition> conditions = DataGenerator.GenerateConditionsForUsers(users);

                dbContext.Set<Condition>().AddRange(conditions);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedAdvertisements(ApplicationDbContext dbContext)
        {
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

            dbContext.ChangeTracker.Clear();
        }

        private static async Task SeedCooperations(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Set<Cooperation>().AnyAsync())
            {
                List<User> users = await dbContext.Set<User>().ToListAsync();

                int cooperationCount = RandomNumberGenerator.GetInt32(3, 5);

                foreach (User buyer in users)
                {
                    foreach (User? seller in users.Where(u => u.Id != buyer.Id))
                    {
                        for (int i = 0; i < cooperationCount; i++)
                        {
                            List<Advertisement> advertisements = await dbContext
                                .Set<Advertisement>()
                                .Include(advertisement => advertisement.Condition)
                                .Where(advertisement => advertisement.Condition.UserId == seller.Id)
                                .ToListAsync();

                            Advertisement advertisement = advertisements[
                                RandomNumberGenerator.GetInt32(advertisements.Count)
                            ];

                            DateTimeOffset scheduledOnUtc = DateTimeOffset.UtcNow.AddDays(
                                RandomNumberGenerator.GetInt32(1, 365)
                            );

                            Money price = advertisement.Price;

                            var status = (CooperationStatus)
                                RandomNumberGenerator.GetInt32(
                                    Enum.GetValues(typeof(CooperationStatus)).Length
                                );

                            var name = new Name($"Cooperation {Guid.NewGuid()}");
                            var description = new Description("Description of cooperation");

                            Result<Cooperation> cooperationResult = Cooperation.Pend(
                                name,
                                description,
                                scheduledOnUtc,
                                price,
                                advertisement,
                                buyer.Id,
                                seller.Id,
                                DateTime.UtcNow
                            );

                            if (cooperationResult.IsFailure)
                            {
                                continue;
                            }

                            Cooperation cooperation = cooperationResult.Value;

                            if (status != CooperationStatus.Pending)
                            {
                                if (status == CooperationStatus.Confirmed)
                                {
                                    cooperation.Confirm(DateTime.UtcNow);
                                }
                                else if (status == CooperationStatus.Done)
                                {
                                    cooperation.Confirm(DateTime.UtcNow);
                                    cooperation.MarkAsDone(DateTime.UtcNow);
                                }
                                else if (status == CooperationStatus.Completed)
                                {
                                    cooperation.Confirm(DateTime.UtcNow);
                                    cooperation.MarkAsDone(DateTime.UtcNow);
                                    cooperation.Complete(DateTime.UtcNow);
                                }
                                else if (status == CooperationStatus.Cancelled)
                                {
                                    cooperation.Confirm(DateTime.UtcNow);
                                    cooperation.Cancel(DateTime.UtcNow);
                                }
                                else if (status == CooperationStatus.Rejected)
                                {
                                    cooperation.Reject(DateTime.UtcNow);
                                }
                            }

                            dbContext.Set<Cooperation>().Add(cooperation);
                        }
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task SeedReviews(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Set<Review>().AnyAsync())
            {
                List<Cooperation> completedCooperations = await dbContext
                    .Set<Cooperation>()
                    .Where(cooperation => cooperation.Status == CooperationStatus.Completed)
                    .ToListAsync();

                foreach (Cooperation cooperation in completedCooperations)
                {
                    if (RandomNumberGenerator.GetInt32(2) == 1)
                    {
                        Review review = DataGenerator.GenerateReview(cooperation);
                        dbContext.Set<Review>().Add(review);
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
