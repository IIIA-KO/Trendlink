using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Common;

namespace Trendlink.Application.Conditions.EditLoggedInUserCondition
{
    public sealed record EditLoggedInUserConditionCommand(Description Description) : ICommand;
}
