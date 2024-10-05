using FluentValidation;
using Trendlink.Application.Extensions;

namespace Trendlink.Application.Reviews.EditReview
{
    internal sealed class EditReviewCommandValidator : AbstractValidator<EditReviewCommand>
    {
        public EditReviewCommandValidator()
        {
            this.RuleFor(c => c.Rating).InclusiveBetween(1, 5);

            this.RuleFor(c => c.Comment).NotNullOrEmpty();
        }
    }
}
