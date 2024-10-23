using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Cooperations;

namespace Trendlink.Application.Cooperations.ConfirmCooperation
{
    public sealed record ConfirmCooperationCommand(CooperationId CooperationId) : ICommand;
}
