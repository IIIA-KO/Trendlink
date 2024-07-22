using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Infrastructure.Specifications.Conditions
{
    internal sealed class ConditionByIdWithAdvertisementsSpecification
        : Specification<Condition, ConditionId>
    {
        public ConditionByIdWithAdvertisementsSpecification(ConditionId conditionId)
            : base(condition => condition.Id == conditionId)
        {
            this.AddInclude(condition => condition.Advertisements);
        }
    }
}
