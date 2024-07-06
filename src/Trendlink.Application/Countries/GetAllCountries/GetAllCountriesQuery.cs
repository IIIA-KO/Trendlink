using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Countries.GetAllCountries
{
    public sealed record GetAllCountriesQuery : IQuery<IReadOnlyCollection<CountryResponse>>;
}
