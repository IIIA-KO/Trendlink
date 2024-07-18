using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Conditions.GetLoggedInUserCondition
{
    public sealed record GetLoggedInUserConditionQuery() : IQuery<ConditionResponse>;
}
