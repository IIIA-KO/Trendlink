using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Users.EditUser
{
    public sealed record EditUserCommand(
        UserId UserId,
        FirstName FirstName,
        LastName LastName,
        DateOnly BirthDate,
        StateId StateId,
        Bio Bio,
        AccountType AccountType,
        AccountCategory AccountCategory
    ) : ICommand;
}
