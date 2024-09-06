using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Domain.UnitTests.InstagramAccounts
{
    public static class InstagramAccountData
    {
        public static readonly UserId UserId = UserId.New();

        public static readonly FacebookPageId FacebookPageId = new FacebookPageId("dummy_id");

        public static Metadata Metadata = new Metadata("dummy_id", 1, "username", 1, 1);
    }
}
