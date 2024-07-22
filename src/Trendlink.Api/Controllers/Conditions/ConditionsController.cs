using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Advertisements.CreateAdvertisement;
using Trendlink.Application.Advertisements.DeleteAdvertisement;
using Trendlink.Application.Advertisements.EditAdvertisement;
using Trendlink.Application.Conditions.CreateCondition;
using Trendlink.Application.Conditions.EditCondition;
using Trendlink.Application.Conditions.GetLoggedInUserCondition;
using Trendlink.Domain.Conditions.Advertisements.ValueObjects;
using Trendlink.Domain.Shared;

namespace Trendlink.Api.Controllers.Conditions
{
    [Route("/api/terms-and-conditions")]
    public class ConditionsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetLoggedInUserConditions(
            CancellationToken cancellationToken
        )
        {
            var query = new GetLoggedInUserConditionQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCondition(
            [FromBody] CreateConditionRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateConditionCommand(new Description(request.Description));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPut]
        public async Task<IActionResult> EditCondition(
            [FromBody] EditConditionRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new EditLoggedInUserConditionCommand(
                new Description(request.Description)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("ad")]
        public async Task<IActionResult> CreateAdvertisement(
            [FromBody] CreateAdvertisementRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new CreateAdvertisementCommand(
                new Name(request.Name),
                new Money(request.PriceAmount, Currency.FromCode(request.PriceCurrency)),
                new Description(request.Description)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPut("ad/{advertisementId:guid}/edit")]
        public async Task<IActionResult> EditAdvertisement(
            Guid advertisementId,
            [FromBody] EditAdvertisementRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new EditAdvertisementCommand(
                new AdvertisementId(advertisementId),
                new Name(request.Name),
                new Money(request.PriceAmount, Currency.FromCode(request.PriceCurrency)),
                new Description(request.Description)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpDelete("ad/{advertisementId:guid}/delete")]
        public async Task<IActionResult> DeleteAdvertisement(
            Guid advertisementId,
            CancellationToken cancellationToken
        )
        {
            var command = new DeleteAdvertisementCommand(new AdvertisementId(advertisementId));

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
