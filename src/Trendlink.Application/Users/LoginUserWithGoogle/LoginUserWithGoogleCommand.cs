using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.LogInUser;

namespace Trendlink.Application.Users.GoogleLogin
{
    public sealed record LogInUserWithGoogleCommand(string Code) : ICommand<AccessTokenResponse>;
}
