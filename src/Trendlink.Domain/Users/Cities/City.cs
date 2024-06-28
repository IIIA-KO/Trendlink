using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Domain.Users.Cities
{
    public sealed class City : Entity<CityId>
    {
        private City(CityId id, CityName name, Country? country)
        {
            this.Id = id;
            this.Name = name;
            this.CountyId = country!.Id;
            this.Country = country;
        }

        private City() { }

        public CityName Name { get; init; }

        public CountryId CountyId { get; init; }

        public Country? Country { get; init; }

        public static Result<City> Create(CityName name, Country? country)
        {
            if (string.IsNullOrEmpty(name.Value) || country is null)
            {
                return Result.Failure<City>(CityErrors.Invalid);
            }

            return new City(CityId.New(), name, country);
        }
    }
}
