﻿using FluentValidation;

namespace Trendlink.Application.Accounts.LogIn
{
    internal sealed class LogInCommandValidator : AbstractValidator<LogInCommand>
    {
        public LogInCommandValidator()
        {
            this.RuleFor(c => c.Email.Value)
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .NotEmpty()
                .WithMessage("Email is required.");

            this.RuleFor(c => c.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password length must be at least 8.")
                .MaximumLength(16)
                .WithMessage("Password length must not exceed 16.")
                .Matches(@"[A-Z]+")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+")
                .WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+")
                .WithMessage("Password must contain at least one number.")
                .Matches(@"[\!\?\*\.\$]+")
                .WithMessage("Password must contain at least one (!? *.$).");
        }
    }
}
