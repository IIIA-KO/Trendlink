using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.Authentication.LogInUser;

namespace Trendlink.Application.Users.Authentication.RefreshToken
{
    public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AccessTokenResponse>;
}
