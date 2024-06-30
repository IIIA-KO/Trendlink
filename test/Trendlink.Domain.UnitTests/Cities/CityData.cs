using Trendlink.Domain.Users.Cities;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Domain.UnitTests.Cities
{
    internal static class CityData
    {
        public static readonly CityName CityName = new("TestCity");

        public static readonly Country Country = Country.Create(new CountryName("TestCountry")).Value;
    }
}
