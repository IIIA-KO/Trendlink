using System.Globalization;
using Bogus;
using Newtonsoft.Json;
using Trendlink.Application.Countries.GetAllCountries;
using Trendlink.Application.Countries.GetStates;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Api.Extensions
{
    public static class DataGenerator
    {
        private static readonly HttpClient HttpClient = new();

        private sealed record ApiResponse<T>(bool Error, string Msg, T Data);

        public sealed record StateDto(string Name, string StateCode);

        private sealed record CountryDto(string Name, string Iso2, List<StateDto> States);

        private static readonly string[] UnsupportedCountries = ["Russia", "Belarus"];

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

                    foreach (StateDto stateDto in countryDto.States)
                    {
                        State state = State.Create(new StateName(stateDto.Name), country).Value;

                        country.States.Add(state);
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
            Faker<User> userFaker = new Faker<User>()
                .RuleFor(u => u.FirstName, f => new FirstName(f.Name.FirstName()))
                .RuleFor(u => u.LastName, f => new LastName(f.Name.LastName()))
                .RuleFor(
                    u => u.BirthDate,
                    f =>
                    {
                        DateTime birthDateTime = f.Date.Past(
                            30,
                            DateTime.Now.AddYears(-18).AddDays(-1)
                        );
                        return new DateOnly(
                            birthDateTime.Year,
                            birthDateTime.Month,
                            birthDateTime.Day
                        );
                    }
                )
                .RuleFor(
                    u => u.Email,
                    (f, u) =>
                        new Email(
                            $"{u.FirstName.Value.ToUpperInvariant()}.{u.LastName.Value.ToUpperInvariant()}@test.com"
                        )
                )
                .RuleFor(
                    u => u.PhoneNumber,
                    f => new PhoneNumber(f.Phone.PhoneNumber("##########"))
                );

            User admin = userFaker.Generate();
            List<User> users = userFaker.Generate(3);

            return (admin, users);
        }
    }
}
