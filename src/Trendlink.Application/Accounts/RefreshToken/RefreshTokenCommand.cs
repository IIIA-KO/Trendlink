using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Accounts.LogInUser;

namespace Trendlink.Application.Accounts.RefreshToken
{
    public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AccessTokenResponse>;
}
