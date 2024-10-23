using FluentValidation;
using Trendlink.Application.Extensions;

namespace Trendlink.Application.Users.EditUser
{
    internal sealed class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            this.RuleFor(c => c.FirstName).NotNullOrEmpty();

            this.RuleFor(c => c.FirstName.Value).NotNullOrEmpty();

            this.RuleFor(c => c.LastName).NotNullOrEmpty();

            this.RuleFor(c => c.LastName.Value).NotNullOrEmpty();

            this.RuleFor(c => c.BirthDate)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.Date.AddYears(-18)))
                .WithMessage("You must be at least 18 years old.");

            this.RuleFor(c => c.StateId).NotEmpty().WithMessage("State is required.");

            this.RuleFor(c => c.AccountCategory)
                .IsInEnum()
                .WithMessage("Invalid account category specified");
        }
    }
}
