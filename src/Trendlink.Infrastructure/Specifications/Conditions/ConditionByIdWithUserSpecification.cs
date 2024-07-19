using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Infrastructure.Specifications.Conditions
{
    internal sealed class ConditionByIdWithUserSpecification : Specification<Condition, ConditionId>
    {
        public ConditionByIdWithUserSpecification(ConditionId conditionId)
            : base(condition => condition.Id == conditionId)
        {
            this.AddInclude(condition => condition.User);
        }
    }
}
