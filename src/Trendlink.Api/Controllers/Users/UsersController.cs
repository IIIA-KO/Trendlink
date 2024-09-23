using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Users.Authentication.LogInUser;
using Trendlink.Application.Users.Authentication.LoginUserWithGoogle;
using Trendlink.Application.Users.Authentication.RefreshToken;
using Trendlink.Application.Users.Authentication.RegisterUser;
using Trendlink.Application.Users.Authentication.RegisterUserWithGoogle;
using Trendlink.Application.Users.DeleteUserAccount;
using Trendlink.Application.Users.EditUser;
using Trendlink.Application.Users.GetUser;
using Trendlink.Application.Users.GetUsers;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Api.Controllers.Users
{
    [Route("/api/users")]
    public class UsersController : BaseApiController
    {
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
            [FromQuery] string? country,
            [FromQuery] string? accountCategory,
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
                country,
                accountCategory,
                minFollowersCount,
                minMediaCount,
                pageNumber,
                pageSize
            );

            return this.HandlePagedResult(await this.Sender.Send(query, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RegisterUserCommand(
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                request.BirthDate,
                new Email(request.Email),
                new PhoneNumber(request.PhoneNumber),
                request.Password,
                new StateId(request.StateId)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn(
            [FromBody] LogInUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new LogInUserCommand(new Email(request.Email), request.Password);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("google-register")]
        public async Task<IActionResult> RegisterUserWithGoogle(
            [FromBody] RegisterUserWithGoogleRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RegisterUserWithGoogleCommand(
                request.Code,
                request.BirthDate,
                new PhoneNumber(request.PhoneNumber),
                new StateId(request.StateId)
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("google-login")]
        public async Task<IActionResult> LoginUserWithGoogle(
            [FromBody] LoginUserWithGoogleRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new LogInUserWithGoogleCommand(request.Code);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RefreshTokenCommand(request.Code);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpPut("{id:guid}/edit")]
        public async Task<IActionResult> EditUser(
            Guid id,
            [FromBody] EditUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new EditUserCommand(
                new UserId(id),
                new FirstName(request.FirstName),
                new LastName(request.LastName),
                request.BirthDate,
                new StateId(request.StateId),
                new Bio(request.Bio),
                request.AccountCategory
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpDelete("delete-my-account")]
        public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
        {
            var command = new DeleteUserAccountCommand();

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
