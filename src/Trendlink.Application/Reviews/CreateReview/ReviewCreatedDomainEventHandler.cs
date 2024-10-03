using MediatR;
using Trendlink.Application.Abstractions.Repositories;
using Trendlink.Domain.Abstraction;
using Trendlink.Domain.Review;
using Trendlink.Domain.Review.DomainEvents;
using Trendlink.Domain.Shared;
using Trendlink.Domain.Users;

namespace Trendlink.Application.Reviews.CreateReview
{
    internal sealed class ReviewCreatedDomainEventHandler
        : INotificationHandler<ReviewCreatedDomainEvent>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewCreatedDomainEventHandler(
            IReviewRepository reviewRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._reviewRepository = reviewRepository;
            this._userRepository = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Handle(
            ReviewCreatedDomainEvent notification,
            CancellationToken cancellationToken
        )
        {
            Review? review = await this._reviewRepository.GetByIdAsync(
                notification.ReviewId,
                cancellationToken
            );
            if (review is null)
            {
                return;
            }

            User? seller = await this._userRepository.GetByIdAsync(
                review.SellerId,
                cancellationToken
            );
            if (seller is null)
            {
                return;
            }

            int reviewCount = await this._reviewRepository.CountUserReviews(
                seller.Id,
                cancellationToken
            );

            double newAverageRating =
                (seller.Rating.Value * (reviewCount - 1) + review.Rating.Value)
                / (double)reviewCount;

            int newRatingValue = (int)Math.Round(newAverageRating);

            Result<Rating> newRatingResult = Rating.Create(newRatingValue);
            if (newRatingResult.IsFailure)
            {
                return;
            }

            if (seller.Rating.Value == newRatingResult.Value.Value)
            {
                return;
            }

            seller.SetRating(newRatingResult.Value);

            await this._unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
