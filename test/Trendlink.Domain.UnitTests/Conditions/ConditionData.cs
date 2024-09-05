using Trendlink.Domain.Shared;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Domain.UnitTests.Conditions
{
    internal static class ConditionData
    {
        public static readonly UserId UserId = UserId.New();

        public static readonly Description Description = new("Description");
    }
}
