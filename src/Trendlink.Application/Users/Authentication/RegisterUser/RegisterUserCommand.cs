using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Users.Authentication.RegisterUser
{
    public sealed record RegisterUserCommand(
        FirstName FirstName,
        LastName LastName,
        DateOnly BirthDate,
        Email Email,
        PhoneNumber PhoneNumber,
        string Password,
        StateId StateId
    ) : ICommand<UserId>;
}
