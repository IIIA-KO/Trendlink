using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Users.Authentication.LogInUser;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Users.Authentication.RegisterUserWithGoogle
{
    public sealed record RegisterUserWithGoogleCommand(
        string Code,
        DateOnly BirthDate,
        PhoneNumber PhoneNumber,
        StateId StateId
    ) : ICommand<AccessTokenResponse>;
}
