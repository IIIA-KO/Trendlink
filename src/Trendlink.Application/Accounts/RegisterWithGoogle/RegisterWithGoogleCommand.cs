using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Accounts.LogInUser;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Accounts.RegisterUserWithGoogle
{
    public sealed record RegisterWithGoogleCommand(
        string Code,
        DateOnly BirthDate,
        PhoneNumber PhoneNumber,
        StateId StateId
    ) : ICommand<AccessTokenResponse>;
}
