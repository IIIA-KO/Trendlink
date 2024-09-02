using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Users.GetUser
{
    public sealed record GetUserQuery(UserId UserId) : IQuery<UserResponse>;
}
