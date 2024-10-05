using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Accounts.LogIn;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Accounts.RegisterWithGoogle
{
    public sealed record RegisterWithGoogleCommand(
        string Code,
        DateOnly BirthDate,
        PhoneNumber PhoneNumber,
        StateId StateId
    ) : ICommand<AccessTokenResponse>;
}
