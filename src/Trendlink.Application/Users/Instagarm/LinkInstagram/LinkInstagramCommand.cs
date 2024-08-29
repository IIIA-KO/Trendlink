using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.Instagarm.LinkInstagram
{
    public sealed record LinkInstagramCommand(string Code) : ICommand;
}
