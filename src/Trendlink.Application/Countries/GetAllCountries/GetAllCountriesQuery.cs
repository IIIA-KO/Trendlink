using Trendlink.Application.Abstractions.Caching;

namespace Trendlink.Application.Countries.GetAllCountries
{
    public sealed record GetAllCountriesQuery : ICachedQuery<IReadOnlyCollection<CountryResponse>>
    {
        public string CacheKey => "countries";

        public TimeSpan? Expiration => TimeSpan.FromDays(1);
    }
}
