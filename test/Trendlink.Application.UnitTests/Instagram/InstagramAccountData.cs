using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.UnitTests.Instagram
{
    public static class InstagramAccountData
    {
        public static InstagramAccount Create() =>
            InstagramAccount.Create(UserId, FacebookPageId, AdvertisementAccountId, Metadata).Value;

        public static readonly UserId UserId = UserId.New();

        public static readonly FacebookPageId FacebookPageId = new("dummy_id");

        public static readonly AdvertisementAccountId AdvertisementAccountId = new("dummy_id");

        public static readonly Metadata Metadata = new("dummy_id", 1, "username", 100, 1);
    }
}
