using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Shared;

namespace Trendlink.Application.Conditions.EditCondition
{
    public sealed record EditLoggedInUserConditionCommand(Description Description) : ICommand;
}
