using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Users.InstagramBusinessAccount
{
    public static class InstagramAccountErrors
    {
        public static readonly Error InvalidFacebookPageId =
            new("InstagramAccount.InvalidFacebookPageId", "Provided Facebook page id is invalid");

        public static readonly Error InvalidId =
            new("InstagramAccount.InvalidId", "The provided id is invalid");

        public static readonly Error InstagramAccountAlreadyLinked =
            new(
                "InstagramAccount.InstagramAccountAlreadyLinked ",
                "User has already linked instagram account"
            );

        public static readonly Error InstagramAccountNotLinked =
            new(
                "InstagramAccount.InstagramAccountNotLinked",
                "Instagram account has not been linked"
            );

        public static readonly Error InstagramAccountDuplicate =
            new(
                "InstagramAccount.InstagramAccountDuplicate",
                "Provided instagram account has already been linked"
            );

        public static readonly Error WrongInstagramAccount =
            new(
                "InstagramAccount.WrongInstagramAccount",
                "User has tried to renew access to different Instagram account"
            );

        public static readonly Error FailedToGetFacebookPage =
            new(
                "InstagramAccount.FailedToGetFacebookPage",
                "Failed to get access to user's Facebook Business Page"
            );

        public static readonly Error IncorrectFacebookPagesCount =
            new(
                "InstagramAccount.IncorrectFacebookPagesCount",
                "User has selected incorrect quantity of Facebook Pages. Required count is exactly one"
            );
    }
}
