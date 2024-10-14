using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Accounts.LogOut
{
    internal sealed class LogOutCommandHandler : ICommandHandler<LogOutCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IKeycloakService _keycloakService;

        public LogOutCommandHandler(
            IUserContext userContext,
            IUserRepository userRepository,
            IKeycloakService keycloakService
        )
        {
            this._userContext = userContext;
            this._userRepository = userRepository;
            this._keycloakService = keycloakService;
        }

        public async Task<Result> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            User user = await this._userRepository.GetByIdWithTokenAsync(
                this._userContext.UserId,
                cancellationToken
            );

            return await this._keycloakService.TerminateUserSession(
                user!.IdentityId,
                cancellationToken
            );
        }
    }
}
