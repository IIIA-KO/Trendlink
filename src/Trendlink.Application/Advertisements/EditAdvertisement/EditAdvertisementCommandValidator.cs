using FluentValidation;
using Trendlink.Application.Extensions;

namespace Trendlink.Application.Advertisements.EditAdvertisement
{
    internal sealed class EditAdvertisementCommandValidator
        : AbstractValidator<EditAdvertisementCommand>
    {
        public EditAdvertisementCommandValidator()
        {
            this.RuleFor(c => c.Price.Amount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price amount must be greater or equal to zero.");

            this.RuleFor(c => c.Price.Currency).NotNullOrEmpty();

            this.RuleFor(c => c.Name).NotNullOrEmpty();

            this.RuleFor(c => c.Name.Value).NotNullOrEmpty();

            this.RuleFor(c => c.Description).NotNullOrEmpty();

            this.RuleFor(c => c.Description.Value).NotNullOrEmpty();
        }
    }
}
