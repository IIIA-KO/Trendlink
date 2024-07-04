using FluentValidation;

namespace Trendlink.Application.Users.EditUser
{
    internal sealed class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            this.RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name is required.");

            this.RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name is required.");

            this.RuleFor(c => c.BirthDate)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.Date.AddYears(-18)))
                .WithMessage("You must be at least 18 years old.");

            this.RuleFor(c => c.StateId).NotEmpty().WithMessage("State is required.");

            this.RuleFor(c => c.AccountType)
                .IsInEnum()
                .WithMessage("Invalid account type specified.");

            this.RuleFor(c => c.AccountCategory)
                .IsInEnum()
                .WithMessage("Invalid account category specified");
        }
    }
}
