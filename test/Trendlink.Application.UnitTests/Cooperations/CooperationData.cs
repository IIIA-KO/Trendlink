using Trendlink.Application.UnitTests.Advertisements;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Cooperations
{
    internal static class CooperationData
    {
        public static readonly Name Name = new("Name");

        public static readonly Description Description = new("Description");

        public static readonly DateTime UtcNow = DateTime.UtcNow;

        public static readonly DateTimeOffset ScheduledOnUtc = DateTimeOffset.UtcNow.AddDays(7);

        public static readonly AdvertisementId AdvertisementId = AdvertisementId.New();

        public static readonly UserId BuyerId = UserId.New();

        public static readonly UserId SellerId = UserId.New();

        public static Cooperation CreatePendingCooperation() =>
            Cooperation
                .Pend(
                    Name,
                    Description,
                    ScheduledOnUtc,
                    AdvertisementData.Price,
                    AdvertisementData.Create(),
                    BuyerId,
                    SellerId,
                    UtcNow
                )
                .Value;

        public static Cooperation CreateConfirmedCooperation()
        {
            Cooperation cooperation = CreatePendingCooperation();
            cooperation.Confirm(DateTime.UtcNow);
            return cooperation;
        }

        public static Cooperation CreateDoneCooperation()
        {
            Cooperation cooperation = CreateConfirmedCooperation();
            cooperation.MarkAsDone(DateTime.UtcNow);
            return cooperation;
        }

        public static Cooperation CreateCompletedCooperation()
        {
            Cooperation cooperation = CreateDoneCooperation();
            cooperation.Complete(DateTime.UtcNow);
            return cooperation;
        }
    }
}
