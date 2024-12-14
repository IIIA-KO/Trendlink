using System.Security.Cryptography;
using Bogus;
using Newtonsoft.Json;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;

namespace Trendlink.Infrastructure.Extensions
{
    public static class DataGenerator
    {
        private static readonly HttpClient HttpClient = new();

        private sealed record ApiResponse<T>(bool Error, string Msg, T Data);

        public sealed record StateDto(string Name, string StateCode);

        private sealed record CountryDto(string Name, string Iso2, List<StateDto> States);

        private static readonly string[] UnsupportedCountries =
        [
            "Russia",
            "Belarus",
            "Iran",
            "North Korea"
        ];

        public static async Task<List<Country>> GenerateCountriesWithStatesAsync(
            Uri countriesApiUrl
        )
        {
            List<Country> countries = [];

            try
            {
                List<CountryDto> countriesWithStates = await GetCountriesWithStatesAsync(
                    countriesApiUrl
                );

                countriesWithStates = countriesWithStates
                    .Where(c => !UnsupportedCountries.Contains(c.Name))
                    .ToList();

                foreach (CountryDto countryDto in countriesWithStates)
                {
                    Country country = Country.Create(new CountryName(countryDto.Name)).Value;

                    if (countryDto.States?.Any() != true)
                    {
                        State state = State.Create(new StateName(countryDto.Name), country).Value;
                        country.States.Add(state);
                    }
                    else
                    {
                        foreach (StateDto stateDto in countryDto.States)
                        {
                            State state = State.Create(new StateName(stateDto.Name), country).Value;

                            country.States.Add(state);
                        }
                    }

                    countries.Add(country);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to generate countries and states: {ex.Message}"
                );
            }

            return countries;
        }

        private static async Task<List<CountryDto>> GetCountriesWithStatesAsync(Uri countriesApiUrl)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(countriesApiUrl);

            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            ApiResponse<List<CountryDto>>? result = JsonConvert.DeserializeObject<
                ApiResponse<List<CountryDto>>
            >(content);

            return result!.Data;
        }

        public static (User admin, List<User> users) GenerateUsers()
        {
            var faker = new Faker();

            User admin = User.Create(
                new FirstName("Trendlink"),
                new LastName("Administrator"),
                new DateOnly(1999, 1, 1),
                StateId.New(),
                new Email("admin@trendlink.com"),
                new PhoneNumber("0123456789")
            ).Value;

            var users = new List<User>();

            for (int i = 0; i < 100; i++)
            {
                users.Add(
                    User.Create(
                        new FirstName(faker.Name.FirstName()),
                        new LastName(faker.Name.LastName()),
                        DateOnly.FromDateTime(faker.Date.Past(30, DateTime.Now.AddYears(-18))),
                        StateId.New(),
                        new Email(faker.Internet.Email()),
                        new PhoneNumber(faker.Random.ReplaceNumbers("##########"))
                    ).Value
                );
            }

            return (admin, users);
        }

        public static List<Condition> GenerateConditionsForUsers(List<User> users)
        {
            var faker = new Faker();
            var conditions = new List<Condition>();

            foreach (User user in users)
            {
                conditions.Add(
                    Condition.Create(user.Id, new Description(faker.Lorem.Paragraphs(3))).Value
                );
            }

            return conditions;
        }

        public static List<Advertisement> GenerateAdvertisementsForCondition(Condition condition)
        {
            var faker = new Faker();
            var advertisements = new List<Advertisement>();

            for (int i = 0; i < 5; i++)
            {
                advertisements.Add(
                    Advertisement
                        .Create(
                            condition.Id,
                            new Name(faker.Lorem.Sentence()),
                            new Money(Math.Round(faker.Random.Decimal(1, 1000), 2), Currency.Usd),
                            new Description(faker.Lorem.Sentence())
                        )
                        .Value
                );
            }

            return advertisements;
        }

        public static Review GenerateReview(Cooperation cooperation)
        {
            var faker = new Faker();

            Rating rating = Rating.Create(RandomNumberGenerator.GetInt32(1, 6)).Value;
            var comment = new Comment(faker.Lorem.Sentence(3));

            Result<Review> reviewResult = Review.Create(
                cooperation,
                rating,
                comment,
                DateTime.UtcNow
            );
            return reviewResult.Value;
        }
    }
}
