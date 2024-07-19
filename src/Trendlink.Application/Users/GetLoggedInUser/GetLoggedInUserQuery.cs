using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.GetLoggedInUser
{
    public sealed record GetLoggedInUserQuery : IQuery<UserResponse>;
}
