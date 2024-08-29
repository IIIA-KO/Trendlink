using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.RenewInstagramAccess
{
    public sealed record RenewInstagramAccessCommand(string Code) : ICommand;
}
