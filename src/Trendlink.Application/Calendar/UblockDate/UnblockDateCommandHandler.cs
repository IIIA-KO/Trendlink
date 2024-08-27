using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users;
using Trendlink.Domain.Users.InstagramBusinessAccount;

namespace Trendlink.Application.Calendar.UblockDate
{
    internal sealed class UnblockDateCommandHandler : ICommandHandler<UnblockDateCommand>
    {
        private readonly IBlockedDateRepository _blockedDateRepository;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public UnblockDateCommandHandler(
            IBlockedDateRepository blockedDateRepository,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork
        )
        {
            this._blockedDateRepository = blockedDateRepository;
            this._userContext = userContext;
            this._dateTimeProvider = dateTimeProvider;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(
            UnblockDateCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Date < DateOnly.FromDateTime(this._dateTimeProvider.UtcNow))
            {
                return Result.Failure(BlockedDateErrors.PastDate);
            }

            UserId userId = this._userContext.UserId;

            BlockedDate? blockedDate = await this._blockedDateRepository.GetByDateAndUserIdAsync(
                request.Date,
                userId,
                cancellationToken
            );
            if (blockedDate is null)
            {
                return Result.Failure(BlockedDateErrors.NotFound);
            }

            if (blockedDate.UserId != userId)
            {
                return Result.Failure(UserErrors.NotAuthorized);
            }

            this._blockedDateRepository.Remove(blockedDate);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
