using Trendlink.Application.UnitTests.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.UnitTests.Cooperations
{
    internal static class CooperationData
    {
        public static Cooperation Create() =>
            Cooperation
                .Pend(
                    Name,
                    Description,
                    ScheduledOnUtc,
                    AdvertisementData.Create(),
                    BuyerId,
                    SellerId,
                    UtcNow
                )
                .Value;

        public static readonly Name Name = new("Name");

        public static readonly Description Description = new("Description");

        public static readonly DateTime UtcNow = DateTime.UtcNow;

        public static readonly DateTimeOffset ScheduledOnUtc = DateTimeOffset.UtcNow.AddDays(7);

        public static readonly AdvertisementId AdvertisementId = AdvertisementId.New();

        public static readonly UserId BuyerId = UserId.New();

        public static readonly UserId SellerId = UserId.New();
    }
}
