using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.LogInUser
{
    public sealed record LogInUserCommand(Email Email, string Password)
        : ICommand<AccessTokenResponse>;
}
