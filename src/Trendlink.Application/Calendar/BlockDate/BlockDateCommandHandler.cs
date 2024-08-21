using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Calendar.BlockDate
{
    internal sealed class BlockDateCommandHandler : ICommandHandler<BlockDateCommand>
    {
        private readonly IBlockedDateRepository _blockedDateRepository;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public BlockDateCommandHandler(
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
            BlockDateCommand request,
            CancellationToken cancellationToken
        )
        {
            if (request.Date < DateOnly.FromDateTime(this._dateTimeProvider.UtcNow))
            {
                return Result.Failure(BlockedDateErrors.PastDate);
            }

            UserId userId = this._userContext.UserId;

            bool blockExists = await this._blockedDateRepository.ExistsByDateAndUserIdAsync(
                request.Date,
                userId,
                cancellationToken
            );
            if (blockExists)
            {
                return Result.Failure(BlockedDateErrors.AlreadyBlocked);
            }

            var blockDate = new BlockedDate(userId, request.Date);

            this._blockedDateRepository.Add(blockDate);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
