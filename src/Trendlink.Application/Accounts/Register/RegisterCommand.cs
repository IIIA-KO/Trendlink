using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Accounts.RegisterUser
{
    public sealed record RegisterCommand(
        FirstName FirstName,
        LastName LastName,
        DateOnly BirthDate,
        Email Email,
        PhoneNumber PhoneNumber,
        string Password,
        StateId StateId
    ) : ICommand<UserId>;
}
