namespace Trendlink.Domain.Conditions.Advertisements.ValueObjects
{
    public sealed record AdvertisementId(Guid Value)
    {
        public static AdvertisementId New() => new(Guid.NewGuid());
    }
}
