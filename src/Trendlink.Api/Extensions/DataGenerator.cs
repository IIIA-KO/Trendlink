using Newtonsoft.Json;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;

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
    }
}
