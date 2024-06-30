using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users.Cities;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string Email,
        string PhoneNumber,
        string Password,
        CityId CityId
    ) : ICommand<UserId>;
}
