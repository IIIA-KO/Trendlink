using FluentValidation;

namespace Trendlink.Application.Users.RegisterUserWithGoogle
{
    internal sealed class RegisterUserWithGoogleCommandValidator
        : AbstractValidator<RegisterUserWithGoogleCommand>
    {
        public RegisterUserWithGoogleCommandValidator()
        {
            this.RuleFor(c => c.BirthDate)
                .LessThan(DateOnly.FromDateTime(DateTime.UtcNow.Date.AddYears(-18)))
                .WithMessage("You must be at least 18 years old.");

            this.RuleFor(p => p.PhoneNumber.Value)
                .NotEmpty()
                .NotNull()
                .WithMessage("Phone Number is required.")
                .MinimumLength(10)
                .WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20)
                .WithMessage("PhoneNumber must not exceed 20 characters.")
                .Matches(@"^\d{10,20}$")
                .WithMessage("PhoneNumber not valid");
        }
    }
}
