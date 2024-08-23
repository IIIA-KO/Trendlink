namespace Trendlink.Domain.Conditions.Advertisements
{
    public sealed record AdvertisementId(Guid Value)
    {
        public static AdvertisementId New() => new(Guid.NewGuid());
    }
}
