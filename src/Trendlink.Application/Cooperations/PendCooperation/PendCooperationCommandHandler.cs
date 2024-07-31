using System.Net.Cache;
using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Exceptions;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Conditions;
using Trendlink.Domain.Conditions.Advertisements;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Cooperations.BlockedDates;
using Trendlink.Domain.Users.ValueObjects;

namespace Trendlink.Application.Cooperations.PendCooperation
{
    internal sealed class PendCooperationCommandHandler
        : ICommandHandler<PendCooperationCommand, CooperationId>
    {
        private readonly IConditionRepository _conditionRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ICooperationRepository _cooperationRepository;
        private readonly IBlockedDateRepository _blockedDateRepository;
        private readonly IUserContext _userContext;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public PendCooperationCommandHandler(
            IConditionRepository conditionRepository,
            IAdvertisementRepository advertisementRepository,
            ICooperationRepository cooperationRepository,
            IBlockedDateRepository blockedDateRepository,
            IUserContext userContext,
            IDateTimeProvider dateTimeProvider,
            IUnitOfWork unitOfWork
        )
        {
            this._conditionRepository = conditionRepository;
            this._advertisementRepository = advertisementRepository;
            this._cooperationRepository = cooperationRepository;
            this._blockedDateRepository = blockedDateRepository;
            this._userContext = userContext;
            this._dateTimeProvider = dateTimeProvider;
            this._unitOfWork = unitOfWork;
        }

        public async Task<Result<CooperationId>> Handle(
            PendCooperationCommand request,
            CancellationToken cancellationToken
        )
        {
            Advertisement? advertisement = await this._advertisementRepository.GetByIdAsync(
                request.AdvertisementId,
                cancellationToken
            );
            if (advertisement is null)
            {
                return Result.Failure<CooperationId>(AdvertisementErrors.NotFound);
            }

            Condition sellerCondition =
                await this._conditionRepository.GetByIdWithAdvertisementsAsync(
                    advertisement.ConditionId,
                    cancellationToken
                );
            if (sellerCondition is null)
            {
                return Result.Failure<CooperationId>(ConditionErrors.NotFound);
            }

            UserId buyerId = this._userContext.UserId;
            if (buyerId == sellerCondition.UserId)
            {
                return Result.Failure<CooperationId>(CooperationErrors.SameUser);
            }

            if (!sellerCondition.HasAdvertisement(advertisement.Name))
            {
                return Result.Failure<CooperationId>(AdvertisementErrors.NotFound);
            }

            bool sellerBlockedDateExists =
                await this._blockedDateRepository.ExistsByDateAndUserIdAsync(
                    DateOnly.FromDateTime(request.ScheduledOnUtc.DateTime),
                    sellerCondition.UserId,
                    cancellationToken
                );
            if (sellerBlockedDateExists)
            {
                return Result.Failure<CooperationId>(CooperationErrors.BlockedDate);
            }

            if (
                await this._cooperationRepository.IsAlreadyStarted(
                    advertisement,
                    request.ScheduledOnUtc,
                    cancellationToken
                )
            )
            {
                return Result.Failure<CooperationId>(CooperationErrors.AlreadyStarted);
            }

            try
            {
                Result<Cooperation> result = Cooperation.Pend(
                    request.Name,
                    request.Description,
                    request.ScheduledOnUtc,
                    advertisement,
                    buyerId,
                    sellerCondition.UserId,
                    this._dateTimeProvider.UtcNow
                );
                if (result.IsFailure)
                {
                    return Result.Failure<CooperationId>(result.Error);
                }

                Cooperation cooperation = result.Value;

                this._cooperationRepository.Add(cooperation);

                await this._unitOfWork.SaveChangesAsync(cancellationToken);

                return cooperation.Id;
            }
            catch (ConcurrencyException)
            {
                return Result.Failure<CooperationId>(CooperationErrors.AlreadyStarted);
            }
        }
    }
}
