using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Instagarm.Posts.GetPosts;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Instagram
{
    [Route("/api/posts")]
    public class InstagramPostsController : BaseApiController
    {
        private readonly IUserContext _userContext;

        public InstagramPostsController(IUserContext userContext)
        {
            this._userContext = userContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoggedInUserPosts(
            [FromQuery] int limit,
            [FromQuery] string? cursorType,
            [FromQuery] string? cursor,
            CancellationToken cancellationToken
        )
        {
            var query = new GetPostsQuery(this._userContext.UserId, limit, cursorType, cursor);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetLoggedInUserPosts(
            Guid userId,
            [FromQuery] int limit,
            [FromQuery] string? cursorType,
            [FromQuery] string? cursor,
            CancellationToken cancellationToken
        )
        {
            var query = new GetPostsQuery(new UserId(userId), limit, cursorType, cursor);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
