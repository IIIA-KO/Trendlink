using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Users;

namespace Trendlink.Application.UnitTests.Conditions
{
    internal static class ConditionData
    {
        public static Condition Create() => Condition.Create(UserId, Description).Value;

        public static readonly ConditionId ConditionId = ConditionId.New();

        public static readonly UserId UserId = UserId.New();

        public static readonly Description Description = new("Description");
    }
}
