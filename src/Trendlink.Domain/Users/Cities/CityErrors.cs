using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.Cities
{
    public static class CityErrors
    {
        public static readonly Error Invalid = new("City.Invalid", "The city is invalid.");

        public static readonly Error NotFound =
            new("City.NotFound", "The city with provided identifier was not found");
    }
}
