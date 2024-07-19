namespace Trendlink.Domain.Users.Countries
{
    public record CountryId(Guid Value)
    {
        public static CountryId New() => new(Guid.NewGuid());
    }
}
