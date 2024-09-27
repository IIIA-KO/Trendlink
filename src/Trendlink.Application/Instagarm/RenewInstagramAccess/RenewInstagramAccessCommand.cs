using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Instagarm.RenewInstagramAccess
{
    public sealed record RenewInstagramAccessCommand(string Code) : ICommand;
}
