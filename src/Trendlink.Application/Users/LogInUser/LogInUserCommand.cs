using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.LogInUser
{
    public sealed record LogInUserCommand(string Email, string Password)
        : ICommand<AccessTokenResponse>;
}
