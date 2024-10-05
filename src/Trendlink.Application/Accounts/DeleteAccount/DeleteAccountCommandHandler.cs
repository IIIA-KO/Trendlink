using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Accounts.DeleteUserAccount
{
    internal sealed class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
    {
        private readonly IUserContext _userContext;
        private readonly IUserRepository _userRepository;
        private readonly IKeycloakService _keycloakService;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountCommandHandler(
            IUserContext userContext,
            IUserRepository userRepository,
            IKeycloakService keycloakService,
            IUnitOfWork unitOfWork
        )
        {
            this._userContext = userContext;
            this._userRepository = userRepository;
            this._keycloakService = keycloakService;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            DeleteAccountCommand request,
            CancellationToken cancellationToken
        )
        {
            User user = await this._userRepository.GetByIdAsync(
                this._userContext.UserId,
                cancellationToken
            );

            Result result = await this._keycloakService.DeleteAccountAsync(
                user!.IdentityId,
                cancellationToken
            );
            if (result.IsFailure)
            {
                return result;
            }

            this._userRepository.Remove(user);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
