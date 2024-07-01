using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users.Countries;

namespace Trendlink.Domain.Users.States
{
    public sealed class State : Entity<StateId>
    {
        private State(StateId id, StateName name, Country? country)
        {
            this.Id = id;
            this.Name = name;
            this.CountyId = country!.Id;
            this.Country = country;
        }

        private State() { }

        public StateName Name { get; init; }

        public CountryId CountyId { get; init; }

        public Country? Country { get; init; }

        public static Result<State> Create(StateName name, Country? country)
        {
            if (name is null || string.IsNullOrEmpty(name.Value) || country is null)
            {
                return Result.Failure<State>(StateErrors.Invalid);
            }

            return new State(StateId.New(), name, country);
        }
    }
}
