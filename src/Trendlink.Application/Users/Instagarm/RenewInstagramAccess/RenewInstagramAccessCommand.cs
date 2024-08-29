using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.Instagarm.RenewInstagramAccess
{
    public sealed record RenewInstagramAccessCommand(string Code) : ICommand;
}
