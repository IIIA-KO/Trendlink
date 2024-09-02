using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Conditions.GetUserCondition
{
    public sealed record GetUserConditionQuery(UserId UserId) : IQuery<ConditionResponse>;
}
