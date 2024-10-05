using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Accounts.LogInUser;

namespace Trendlink.Application.Accounts.LoginUserWithGoogle
{
    public sealed record LoginWithGoogleCommand(string Code) : ICommand<AccessTokenResponse>;
}
