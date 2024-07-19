using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Domain.Conditions.Advertisements
{
    public sealed class Advertisement : Entity<AdvertisementId>
    {
        private Advertisement() { }

        private Advertisement(
            AdvertisementId id,
            ConditionId conditionId,
            Name name,
            Money price,
            Description description
        )
            : base(id)
        {
            this.ConditionId = conditionId;
            this.Name = name;
            this.Price = price;
            this.Description = description;
        }

        public ConditionId ConditionId { get; set; }

        public Condition Condition { get; set; }

        public Name Name { get; private set; }

        public Money Price { get; private set; }

        public Description Description { get; private set; }

        public static Result<Advertisement> Create(
            ConditionId conditionId,
            Name name,
            Money price,
            Description description
        )
        {
            Result validationResult = ValidateParameters(name, price, description);
            if (validationResult.IsFailure)
            {
                return Result.Failure<Advertisement>(validationResult.Error);
            }

            return new Advertisement(AdvertisementId.New(), conditionId, name, price, description);
        }

        public Result Update(Name name, Money price, Description description)
        {
            Result validationResult = ValidateParameters(name, price, description);
            if (validationResult.IsFailure)
            {
                return Result.Failure(validationResult.Error);
            }

            this.Name = name;
            this.Price = price;
            this.Description = description;

            return Result.Success();
        }

        private static Result ValidateParameters(Name name, Money price, Description description)
        {
            if (
                name is null
                || string.IsNullOrEmpty(name.Value)
                || description is null
                || string.IsNullOrEmpty(description.Value)
            )
            {
                return Result.Failure<Advertisement>(AdvertisementErrors.Invalid);
            }

            if (price.Amount <= 0)
            {
                return Result.Failure<Advertisement>(AdvertisementErrors.InvalidPrice);
            }

            return Result.Success();
        }
    }
}
