using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Application.Notifications.GetLoggedInUserNotifications;
using Trendlink.Application.Notifications.GetUserNotifications;
using Trendlink.Application.Users.EditUser;
using Trendlink.Application.Users.GetLoggedInUser;
using Trendlink.Application.Users.LinkInstagram;
using Trendlink.Application.Users.LogInUser;
using Trendlink.Application.Users.LoginUserWithGoogle;
using Trendlink.Application.Users.RefreshToken;
using Trendlink.Application.Users.RegisterUser;
using Trendlink.Application.Users.RegisterUserWithGoogle;
using Trendlink.Domain.Users.States;

namespace Trendlink.Api.Controllers.Users
{
    [Route("/api/users")]
    public class UsersController : BaseApiController
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetLoggedInUser(CancellationToken cancellationToken)
        {
            var query = new GetLoggedInUserQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
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

        [HttpPost("link-instagram")]
        public async Task<IActionResult> LinkInstagram(
            [FromBody] LinkInstagramRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new LinkInstagramCommand(request.Code);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetLoggedInUserNotifications(
            CancellationToken cancellationToken
        )
        {
            var query = new GetLoggedInUserNotificationsQuery();

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
        }

        [HttpGet("{id:guid}/notifications")]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> GetUserNotifications(
            Guid id,
            CancellationToken cancellationToken
        )
        {
            var query = new GetUserNotificationsQuery(new UserId(id));

            return this.HandleResult(await this.Sender.Send(query, cancellationToken));
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
                request.AccountType,
                request.AccountCategory
            );

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
