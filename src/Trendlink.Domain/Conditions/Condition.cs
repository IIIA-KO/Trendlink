using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Conditions.ValueObjects;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.Conditions
{
    public sealed class Condition : Entity<ConditionId>
    {
        private Condition() { }

        private Condition(ConditionId id, UserId userId, Description description)
            : base(id)
        {
            this.UserId = userId;
            this.Description = description;
        }

        private readonly List<Advertisement> _advertisements = [];

        public UserId UserId { get; private set; }

        public User? User { get; private set; }

        public Description Description { get; private set; }

        public IReadOnlyCollection<Advertisement> Advertisements =>
            this._advertisements.AsReadOnly();

        public static Result<Condition> Create(UserId userId, Description description)
        {
            if (description is null || string.IsNullOrEmpty(description.Value))
            {
                return Result.Failure<Condition>(ConditionErrors.InvalidDescription);
            }

            return new Condition(ConditionId.New(), userId, description);
        }
    }
}
