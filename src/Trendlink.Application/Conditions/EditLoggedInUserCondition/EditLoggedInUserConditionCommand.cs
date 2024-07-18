using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Conditions.ValueObjects;

namespace Trendlink.Application.Conditions.EditCondition
{
    public sealed record EditLoggedInUserConditionCommand(Description Description) : ICommand;
}
