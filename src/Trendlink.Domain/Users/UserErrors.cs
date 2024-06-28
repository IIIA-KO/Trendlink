using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users
{
    public static class UserErrors
    {
        public static readonly Error NotFound =
            new("User.NotFound", "The user with the specified identifier was not found");

        public static readonly Error InvalidCredentials =
            new("User.InvalidCredentials", "The provided credentials were invalid");
    }
}
