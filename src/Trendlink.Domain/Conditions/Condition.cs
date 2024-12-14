using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Users;

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

        public User? User { get; init; }

        public Description Description { get; private set; }

        public IReadOnlyCollection<Advertisement> Advertisements =>
            this._advertisements.AsReadOnly();

        public static Result<Condition> Create(UserId userId, Description description)
        {
            Result validationResult = ValidateParameters(description);
            if (validationResult.IsFailure)
            {
                return Result.Failure<Condition>(validationResult.Error);
            }

            return new Condition(ConditionId.New(), userId, description);
        }

        public Result Update(Description description)
        {
            Result validationResult = ValidateParameters(description);
            if (validationResult.IsFailure)
            {
                return Result.Failure(validationResult.Error);
            }

            this.Description = description;

            return Result.Success();
        }

        private static Result ValidateParameters(Description description)
        {
            if (description is null || string.IsNullOrEmpty(description.Value))
            {
                return Result.Failure<Condition>(ConditionErrors.InvalidDescription);
            }

            return Result.Success();
        }

        public bool HasAdvertisement(Name advertisementName)
        {
            return this._advertisements.Exists(ad =>
                string.Equals(ad.Name.Value, advertisementName.Value, StringComparison.Ordinal)
            );
        }
    }
}
