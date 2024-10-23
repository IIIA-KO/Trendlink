using Trendlink.Domain.Users.Countries;

namespace Trendlink.Infrastructure.Specifications.Countries
{
    internal sealed class CountryByIdWithStatesSpecification : Specification<Country, CountryId>
    {
        public CountryByIdWithStatesSpecification(CountryId countryId)
            : base(country => country.Id == countryId)
        {
            this.AddInclude(country => country.States);
        }
    }
}
