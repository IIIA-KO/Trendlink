using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Instagarm.LinkInstagram;
using Trendlink.Application.Instagarm.RenewInstagramAccess;

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
    }
}
