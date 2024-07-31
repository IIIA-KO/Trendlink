using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.CancelCooperation
{
    public sealed record CancelCooperationCommand(CooperationId CooperationId) : ICommand;
}
