using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users.States;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.RegisterUser
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
