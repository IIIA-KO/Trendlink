using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Shared;

namespace Trendlink.Domain.UnitTests.Advertisements
{
    internal static class AdvertisementData
    {
        public static Advertisement Create() =>
            Advertisement.Create(ConditionId, Name, Price, Description).Value;

        public static readonly ConditionId ConditionId = ConditionId.New();

        public static readonly Name Name = new("Name");

        public static readonly Money Price = new(1m, Currency.Uah);

        public static readonly Description Description = new("Description");
    }
}
