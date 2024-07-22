using Trendlink.Domain.Conditions;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Infrastructure.Specifications.Conditions
{
    internal sealed class ConditionByUserIdWithAdvertisementsSpecification
        : Specification<Condition, ConditionId>
    {
        public ConditionByUserIdWithAdvertisementsSpecification(UserId userId)
            : base(condition => condition.UserId == userId)
        {
            this.AddInclude(condition => condition.Advertisements);
        }
    }
}
