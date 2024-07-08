using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.States;

namespace Trendlink.Domain.Users.Countries
{
    public sealed class Country : Entity<CountryId>
    {
        private Country(CountryId id, CountryName name)
        {
            this.Id = id;
            this.Name = name;
        }

        public CountryName Name { get; init; }

        public ICollection<State> States { get; init; } = [];

        public static Result<Country> Create(CountryName name)
        {
            if (name is null || string.IsNullOrEmpty(name.Value))
            {
                return Result.Failure<Country>(CountryErrors.Invalid);
            }

            return new Country(CountryId.New(), name);
        }
    }
}
