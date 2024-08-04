using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;

namespace Trendlink.Application.Users.GoogleLogin
{
    public sealed record LoginUserWithGoogleCommand(string Code) : ICommand<AccessTokenResponse>;
}
