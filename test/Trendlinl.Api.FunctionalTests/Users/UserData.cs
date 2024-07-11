using System.Globalization;
using Trendlink.Api.Controllers.Users;

namespace Trendlink.Api.FunctionalTests.Users
{
    internal static class UserData
    {
        public static readonly DateOnly ValidBirthDate = DateOnly.FromDateTime(
            DateTime.Now.AddYears(-18).AddDays(-1)
        );

        public static readonly string StringValidBirthDate = ValidBirthDate.ToString(
            CultureInfo.InvariantCulture
        );

        public static RegisterUserRequest RegisterTestUserRequest =>
            new(
                "first",
                "last",
                ValidBirthDate,
                "test@test.com",
                "0123456789",
                "Pa$$w0rd",
                Guid.NewGuid()
            );
    }
}
