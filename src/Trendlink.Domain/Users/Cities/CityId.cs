namespace Trendlink.Domain.Users.Cities
{
    public record CityId(Guid Value)
    {
        public static CityId New() => new(Guid.NewGuid());
    }
}
