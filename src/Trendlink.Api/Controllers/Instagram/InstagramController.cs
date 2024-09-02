using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Users.Instagarm.GetUserPosts;
using Trendlink.Application.Users.Instagarm.LinkInstagram;
using Trendlink.Application.Users.Instagarm.RenewInstagramAccess;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Instagram
{
    [Route("/api/instagram")]
    public class InstagramController : BaseApiController
    {
        [HttpPost("link-account")]
        public async Task<IActionResult> LinkInstagram(
            [FromBody] LinkInstagramRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new LinkInstagramCommand(request.Code);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("renew-access")]
        public async Task<IActionResult> RenewInstagramAccess(
            [FromBody] RenewInstagramAccessRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RenewInstagramAccessCommand(request.Code);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpGet("{userId:guid}/posts")]
        public async Task<IActionResult> GetUserPosts(
            Guid userId,
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserPostsQuery(new UserId(userId));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
