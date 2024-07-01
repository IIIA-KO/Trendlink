using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;

namespace Trendlink.Domain.UnitTests.States
{
    internal static class StateData
    {
        public static readonly StateName StateName = new("TestState");

        public static readonly Country Country = Country.Create(new CountryName("TestCountry")).Value;
    }
}
