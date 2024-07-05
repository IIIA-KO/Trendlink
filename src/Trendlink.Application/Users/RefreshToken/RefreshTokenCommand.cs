using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.RefreshToken
{
    public sealed record RefreshTokenCommand(string RefreshToken) : ICommand<AccessTokenResponse>;
}
