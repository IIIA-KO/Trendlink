﻿using FluentValidation;

namespace Trendlink.Application.Conditions.EditLoggedInUserCondition
{
    internal sealed class EditLoggedInUserConditionCommandValidator
        : AbstractValidator<EditLoggedInUserConditionCommand>
    {
        public EditLoggedInUserConditionCommandValidator()
        {
            this.RuleFor(c => c.Description).NotEmpty().WithMessage("Description is required");
        }
    }
}
