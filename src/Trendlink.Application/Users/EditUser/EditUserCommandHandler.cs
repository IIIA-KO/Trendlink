using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.States;

namespace Trendlink.Application.Users.EditUser
{
    internal sealed class EditUserCommandHandler : ICommandHandler<EditUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public EditUserCommandHandler(
            IUserRepository userRepository,
            IStateRepository stateRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepository = userRepository;
            this._stateRepository = stateRepository;
            this._userContext = userContext;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            EditUserCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await this._userRepository.GetByIdWithRolesAsync(
                request.UserId,
                cancellationToken
            );
            if (user is null)
            {
                return Result.Failure(UserErrors.NotFound);
            }

            if (
                this._userContext.IdentityId != user.IdentityId
                && !user.HasRole(Role.Administrator)
            )
            {
                return Result.Failure(UserErrors.NotAuthorized);
            }

            State state = await this._stateRepository.GetByIdAsync(
                request.StateId,
                cancellationToken
            );
            if (state is null)
            {
                return Result.Failure(StateErrors.NotFound);
            }

            Result result = user.Update(
                request.FirstName,
                request.LastName,
                request.BirthDate,
                state,
                request.Bio,
                request.AccountType,
                request.AccountCategory
            );

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            return Result.Success(await this._unitOfWork.SaveChangesAsync(cancellationToken));
        }
    }
}
