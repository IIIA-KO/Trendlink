using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Accounts.ResendEmailVerificationToken
{
    public sealed record ResendEmailVerificationTokenCommand(Email Email) : ICommand;
}
