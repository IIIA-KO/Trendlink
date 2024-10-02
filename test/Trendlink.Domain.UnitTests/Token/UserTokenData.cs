using Trendlink.Domain.Users;

namespace Trendlink.Domain.UnitTests.Token
{
    public static class UserTokenData
    {
        public static readonly UserId UserId = UserId.New();

        public static string AccessToken = "access_token";

        public static DateTimeOffset ExpiresIn = DateTimeOffset.UtcNow.AddMonths(3);
    }
}
