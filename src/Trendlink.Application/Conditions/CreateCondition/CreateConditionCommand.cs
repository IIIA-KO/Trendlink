using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Application.Conditions.CreateCondition
{
    public sealed record CreateConditionCommand(Description Description) : ICommand<ConditionId>;
}
