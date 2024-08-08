using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Users.LinkInstagram
{
    public sealed record LinkInstagramCommand(string Code) : ICommand;
}
