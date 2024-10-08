using Trendlink.Application.Abstractions.Messaging;

namespace Trendlink.Application.Instagarm.LinkInstagram
{
    public sealed record LinkInstagramCommand(string Code) : ICommand;
}
