using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Trendlink.Api.Controllers.Users;
using Trendlink.Application.Accounts.DeleteUserAccount;
using Trendlink.Application.Accounts.LogInUser;
using Trendlink.Application.Accounts.LoginUserWithGoogle;
using Trendlink.Application.Accounts.RefreshToken;
using Trendlink.Application.Accounts.RegisterUser;
using Trendlink.Application.Accounts.RegisterUserWithGoogle;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Api.Controllers.Accounts
{
    [Route("/api/accounts")]
    public class AccountsController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RegisterCommand(
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
            var command = new LogInCommand(new Email(request.Email), request.Password);

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }

        [AllowAnonymous]
        [HttpPost("google-register")]
        public async Task<IActionResult> RegisterUserWithGoogle(
            [FromBody] RegisterUserWithGoogleRequest request,
            CancellationToken cancellationToken
        )
        {
            var command = new RegisterWithGoogleCommand(
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
            var command = new LoginWithGoogleCommand(request.Code);

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

        [HttpDelete]
        public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
        {
            var command = new DeleteAccountCommand();

            return this.HandleResult(await this.Sender.Send(command, cancellationToken));
        }
    }
}
