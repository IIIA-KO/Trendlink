using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.Authentication.LogInUser;

namespace Trendlink.Application.Users.Authentication.LoginUserWithGoogle
{
    public sealed record LogInUserWithGoogleCommand(string Code) : ICommand<AccessTokenResponse>;
}
