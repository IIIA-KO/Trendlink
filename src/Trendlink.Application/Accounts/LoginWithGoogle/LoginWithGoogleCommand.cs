using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Accounts.LogIn;

namespace Trendlink.Application.Accounts.LoginWithGoogle
{
    public sealed record LoginWithGoogleCommand(string Code) : ICommand<AccessTokenResponse>;
}
