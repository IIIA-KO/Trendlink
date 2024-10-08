﻿using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;

namespace Trendlink.Domain.UnitTests.Users
{
    internal static class UserData
    {
        public const int MinimumAge = 18;

        public static readonly FirstName FirstName = new("First");

        public static readonly LastName LastName = new("Last");

        public static readonly Email Email = new("test@test.com");

        public static readonly DateOnly BirthDate = DateOnly.FromDateTime(
            DateTime.Now.AddYears(-MinimumAge)
        );

        public static readonly Photo ProfilePicture =
            new("picture-id", new Uri("https://example.com/profile.jpg"));

        public static readonly PhoneNumber PhoneNumber = new("0123456789");

        public static readonly State State = State
            .Create(new StateName("State"), Country.Create(new CountryName("Country")).Value)
            .Value;

        public static readonly Bio Bio = new("Bio");
    }
}
