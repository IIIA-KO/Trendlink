using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.Countries
{
    public static class CountryErrors
    {
        public static readonly Error Invalid = new("Country.Invalid", "The country is invalid.");

        public static readonly Error NotFound =
            new("Country.NotFound", "The country with provided identifier was not found");
    }
}
