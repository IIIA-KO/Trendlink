using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Cooperations.CancelCooperation;
using Trendlink.Application.Cooperations.CompleteCooperation;
using Trendlink.Application.Cooperations.ConfirmCooperation;
using Trendlink.Application.Cooperations.MarkCooperationAsDone;
using Trendlink.Application.Cooperations.PendCooperation;
using Trendlink.Application.Cooperations.RejectCooperation;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Shared;

namespace Trendlink.Api.Controllers.Cooperations
{
    [Route("api/cooperations")]
    public class CooperationsController : BaseApiController
    {
        [HttpPost("request-cooperation")]
        public async Task<IActionResult> PendCooperation(
            PendCooperationRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new PendCooperationCommand(
                new Name(request.Name),
                new Description(request.Description),
                request.ScheduledOnUtc,
                new Money(request.PriceAmount, Currency.FromCode(request.PriceCurrency)),
                new AdvertisementId(request.AdvertisementId)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("{id:guid}/confirm")]
        public async Task<IActionResult> ConfirmCooperation(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var command = new ConfirmCooperationCommand(new CooperationId(id));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("{id:guid}/reject")]
        public async Task<IActionResult> RejectCooperation(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var command = new RejectCooperationCommand(new CooperationId(id));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("{id:guid}/cancel")]
        public async Task<IActionResult> CancelCooperation(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var command = new CancelCooperationCommand(new CooperationId(id));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("{id:guid}/mark-as-done")]
        public async Task<IActionResult> MarkCooperationAsDone(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var command = new MarkCooperationAsDoneCommand(new CooperationId(id));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("{id:guid}/complete")]
        public async Task<IActionResult> CompleteCooperation(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var command = new CompleteCooperationCommand(new CooperationId(id));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
