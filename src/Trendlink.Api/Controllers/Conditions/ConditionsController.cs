using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Conditions.CreateCondition;
using Trendlink.Application.Conditions.EditLoggedInUserCondition;
using Trendlink.Application.Conditions.GetUserCondition;
using Trendlink.Domain.Common;
using Trendlink.Domain.Users;

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
            var query = new GetUserConditionQuery(this.UserContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserConditions(
            [FromRoute] Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserConditionQuery(new UserId(userId));

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
    }
}
