using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Cooperations.ConfirmCooperation
{
    internal sealed class ConfirmCooperationCommandHandler
        : ICommandHandler<ConfirmCooperationCommand>
    {
        private readonly ICooperationRepository _cooperationRepository;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmCooperationCommandHandler(
            ICooperationRepository cooperationRepository,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork
        )
        {
            this._cooperationRepository = cooperationRepository;
            this._userContext = userContext;
            this._dateTimeProvider = dateTimeProvider;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            ConfirmCooperationCommand request,
            CancellationToken cancellationToken
        )
        {
            Cooperation? cooperation = await this._cooperationRepository.GetByIdAsync(
                request.CooperationId,
                cancellationToken
            );
            if (cooperation is null)
            {
                return Result.Failure(CooperationErrors.NotFound);
            }

            if (cooperation.ScheduledOnUtc <= this._dateTimeProvider.UtcNow)
            {
                return Result.Failure(CooperationErrors.InvalidTime);
            }

            if (cooperation.SellerId != this._userContext.UserId)
            {
                return Result.Failure(UserErrors.NotAuthorized);
            }

            Result result = cooperation.Confirm(this._dateTimeProvider.UtcNow);
            if (result.IsFailure)
            {
                return result;
            }

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
