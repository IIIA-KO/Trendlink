using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.RelinkInstagram
{
    public sealed record RenewInstagramAccessCommand(string Code) : ICommand;
}
