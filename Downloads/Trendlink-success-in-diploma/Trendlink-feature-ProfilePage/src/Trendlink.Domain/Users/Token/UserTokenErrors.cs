using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.Token
{
    public static class UserTokenErrors
    {
        public static readonly Error AccessTokenInvalid =
            new("UserToken.AccessTokenInvalid", "The provided access token is invalid");
    }
}
