﻿using Trendlink.Domain.Users;
using Trendlink.Domain.Users.Countries;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.IntegrationTests.Users
{
    internal static class UserData
    {
        public const int MinimumAge = 18;

        public const string Password = "Pa$$w0rd";

        public static User Create() =>
            User.Create(FirstName, LastName, BirthDate, State.Id, Email, PhoneNumber).Value;

        public static readonly FirstName FirstName = new("First");

        public static readonly LastName LastName = new("Last");

        public static readonly Email Email = new("test@test.com");

        public static readonly DateOnly BirthDate = DateOnly.FromDateTime(
            DateTime.Now.AddYears(-MinimumAge)
        );

        public static readonly PhoneNumber PhoneNumber = new("0123456789");

        public static readonly State State = State
            .Create(new StateName("State"), Country.Create(new CountryName("Country")).Value)
            .Value;
    }
}
