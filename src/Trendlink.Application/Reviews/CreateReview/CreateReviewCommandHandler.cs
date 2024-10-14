using Trendlink.Application.Abstractions.Authentication;
using Trendlink.Application.Abstractions.Clock;
using Trendlink.Application.Abstractions.Messaging;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Cooperations;
using Trendlink.Domain.Reviews;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Reviews.CreateReview
{
    internal sealed class CreateReviewCommandHandler : ICommandHandler<CreateReviewCommand>
    {
        private readonly ICooperationRepository _cooperationRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateReviewCommandHandler(
            ICooperationRepository cooperationRepository,
            IReviewRepository reviewRepository,
            IUserContext userContext,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider
        )
        {
            this._cooperationRepository = cooperationRepository;
            this._reviewRepository = reviewRepository;
            this._userContext = userContext;
            this._unitOfWork = unitOfWork;
            this._dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(
            CreateReviewCommand request,
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

            if (this._userContext.UserId != cooperation.BuyerId)
            {
                return Result.Failure(UserErrors.NotAuthorized);
            }

            Review? review = await this._reviewRepository.GetByCooperationIdAndBuyerIdAsync(
                request.CooperationId,
                cooperation.BuyerId,
                cancellationToken
            );
            if (review is not null)
            {
                return Result.Failure(ReviewErrors.AlreadyReviewed);
            }

            Result<Rating> ratingResult = Rating.Create(request.Rating);
            if (ratingResult.IsFailure)
            {
                return ratingResult;
            }

            Result<Review> reviewResult = Review.Create(
                cooperation,
                ratingResult.Value,
                request.Comment,
                this._dateTimeProvider.UtcNow
            );
            if (reviewResult.IsFailure)
            {
                return reviewResult;
            }

            this._reviewRepository.Add(reviewResult.Value);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
