using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Application.Countries.GetAllCountries
{
    public sealed record GetAllCountriesQuery : IQuery<IReadOnlyCollection<CountryResponse>>;
}
