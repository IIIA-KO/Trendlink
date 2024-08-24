using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.InstagramBusinessAccount
{
    public static class InstagramAccountErrors
    {
        public static readonly Error InvalidFacebookPageId =
            new("InstagramAccount.InvalidFacebookPageId", "Provided Facebook page id is invalid");

        public static readonly Error InvalidId =
            new("InstagramAccount.InvalidId", "The provided id is invalid");
    }
}
