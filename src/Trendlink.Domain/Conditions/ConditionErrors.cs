using Trendlink.Domain.Abstraction;

namespace Trendlink.Domain.Conditions
{
    public static class ConditionErrors
    {
        public static readonly NotFoundError NotFound =
            new("Condition.NotFound", "The condition with the specified identifier was not found");

        public static readonly Error InvalidDescription =
            new("Condition.InvalidDescription", "The provided description is invalid");
    }
}
