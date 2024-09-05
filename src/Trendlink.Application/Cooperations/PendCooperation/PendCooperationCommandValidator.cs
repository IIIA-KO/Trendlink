using FluentValidation;
using Trendlink.Application.Extensions;

namespace Trendlink.Application.Cooperations.PendCooperation
{
    internal sealed class PendCooperationCommandValidator
        : AbstractValidator<PendCooperationCommand>
    {
        public PendCooperationCommandValidator()
        {
            this.RuleFor(c => c.Name).NotNullOrEmpty();

            this.RuleFor(c => c.Name.Value).NotNullOrEmpty();

            this.RuleFor(c => c.Description).NotNullOrEmpty();

            this.RuleFor(c => c.Description.Value).NotNullOrEmpty();

            this.RuleFor(c => c.Price.Amount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price amount must be greater or equal to zero.");

            this.RuleFor(c => c.Price.Currency).NotNullOrEmpty();
        }
    }
}
