using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Accounts.VerifyEmail
{
    public sealed record VerifyEmailCommand(Guid Token) : ICommand;
}
