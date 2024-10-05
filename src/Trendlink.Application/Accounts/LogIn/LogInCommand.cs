using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Accounts.LogInUser
{
    public sealed record LogInCommand(Email Email, string Password) : ICommand<AccessTokenResponse>;
}
