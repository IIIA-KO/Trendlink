using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Users
{
    internal static class UserData
    {
        public const int MinimumAge = 18;

        public const string Password = "Pa$$w0rd";

        public static User Create() =>
            User.Create(FirstName, LastName, BirthDate, State.Id, Email, PhoneNumber).Value;

        public static readonly AccessTokenResponse Token =
            new("access_token", "refresh_token", 900);

        public static readonly FirstName FirstName = new("First");

        public static readonly LastName LastName = new("Last");

        public static readonly Email Email = new("test@test.com");

        public static readonly DateOnly BirthDate = DateOnly.FromDateTime(
            DateTime.Now.AddYears(-18)
        );

        public static readonly PhoneNumber PhoneNumber = new("0123456789");

        public static readonly State State = State
            .Create(new StateName("State"), Country.Create(new CountryName("Country")).Value)
            .Value;

        public static readonly Bio Bio = new("Bio");

        public static readonly AccountType AccountType = AccountType.Personal;

        public static readonly AccountCategory AccountCategory = AccountCategory.Education;
    }
}
