using FluentValidation;
using Trendlink.Application.Extensions;

namespace Trendlink.Application.Reviews.CreateReview
{
    internal sealed class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewCommandValidator()
        {
            this.RuleFor(c => c.Rating).InclusiveBetween(1, 5);

            this.RuleFor(c => c.Comment).NotNullOrEmpty();
        }
    }
}
