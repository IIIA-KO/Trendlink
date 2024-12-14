using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Common;
using Trendlink.Domain.Conditions;

namespace Trendlink.Application.Conditions.CreateCondition
{
    public sealed record CreateConditionCommand(Description Description) : ICommand<ConditionId>;
}
