using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;

namespace Trendlink.Application.Users.RefreshToken
{
    public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AccessTokenResponse>;
}
