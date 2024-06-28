using Trendlink.Domain.Users.Cities;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.UnitTests.Users
{
    internal static class UserData
    {
        public static readonly FirstName FirstName = new("First");
        
        public static readonly LastName LastName = new("Last");
        
        public static readonly Email Email = new("test@test.com");
        
        public static readonly DateOnly BirthDate = DateOnly.FromDateTime(DateTime.Now.AddYears(-18));
        
        public static readonly PhoneNumber PhoneNumber = new("0123456789");

        public static readonly City City = City.Create(new CityName("City"), Country.Create(new CountryName("Country")).Value).Value;
    }
}
