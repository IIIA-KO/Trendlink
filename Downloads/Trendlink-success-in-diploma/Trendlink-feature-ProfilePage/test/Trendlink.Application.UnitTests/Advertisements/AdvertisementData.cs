using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Shared;

namespace Trendlink.Application.UnitTests.Advertisements
{
    internal static class AdvertisementData
    {
        public static Advertisement Create() =>
            Advertisement.Create(ConditionId, Name, Price, Description).Value;

        public static Advertisement Create(decimal price, string currencyCode) =>
            Advertisement
                .Create(
                    ConditionId,
                    Name,
                    new Money(price, Currency.FromCode(currencyCode)),
                    Description
                )
                .Value;

        public static readonly AdvertisementId AdvertisementId = AdvertisementId.New();

        public static readonly ConditionId ConditionId = ConditionId.New();

        public static readonly Name Name = new("Name");

        public static readonly Money Price = new(100, Currency.FromCode("UAH"));

        public static readonly Description Description = new("Description");
    }
}
