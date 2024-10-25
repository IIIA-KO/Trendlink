using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Users.GetUser;
using Trendlink.Application.Users.GetUsers;
using Trendlink.Domain.Users;

namespace Trendlink.Api.Controllers.Users
{
    [Route("/api/users")]
    public class UsersController : BaseApiController
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetUserQuery(this.UserContext.UserId);

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(
            [FromRoute] Guid id,
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserQuery(new UserId(id));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(
            [FromQuery] string? searchTerm,
            [FromQuery] string? sortColumn,
            [FromQuery] string? sortOrder,
            [FromQuery] AccountCategory? accountCategory,
            [FromQuery] int minFollowersCount = 0,
            [FromQuery] int minMediaCount = 0,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default
        )
        {
            var query = new GetUsersQuery(
                searchTerm,
                sortColumn,
                sortOrder,
                accountCategory,
                minFollowersCount,
                minMediaCount,
                pageNumber,
                pageSize
            );

            return this.HandlePagedResult(await this.Sender.Send(query, cancellationToken));
        }
    }
}
