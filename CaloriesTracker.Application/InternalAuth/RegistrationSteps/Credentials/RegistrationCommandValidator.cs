using CaloriesTracker.Domain.Validation.Rules;
using FluentValidation;
using System;

namespace CaloriesTracker.Application.User.RegistrationSteps.Credentials
{
    internal sealed class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).Must(x => x.IsEmailValid()).WithMessage("Email is not valid");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .Must(x => x.DoesGreaterOrEqualThen(10)).WithMessage("Password must be greater then 10")
                .Must(x => x.DoesContainDigit()).WithMessage("Password must contain at least one digit")
                .Must(x => x.DoesContainNonAlphanumeric()).WithMessage("Password must contain a non alphanumeric character");
            RuleFor(x => x.Goal).Must(x => x.IsValid()).WithMessage("Goal is not specified");
            RuleFor(x => x.Gender).Must(x => x.IsValid()).WithMessage("Gender is not specified");
            RuleFor(x => x.CurrentWeight).GreaterThan(0).WithMessage("Current weight must be greater than 0")
               .GreaterThan(x => x.TargetWeight).When(x => x.Goal == Domain.User.GoalType.LooseWeight).WithMessage("Current weight should be greater than target weight")
               .LessThan(x => x.TargetWeight).When(x => x.Goal == Domain.User.GoalType.GainWeight).WithMessage("Current weight should be less than target weight");
            RuleFor(x => x.TargetWeight).GreaterThan(0).When(x => x.Goal != Domain.User.GoalType.SaveWeight).WithMessage("Target weight must be greater than 0")
                .GreaterThan(x => x.CurrentWeight).When(x => x.Goal == Domain.User.GoalType.GainWeight).WithMessage("Target weight should be greater than current weight")
                .LessThan(x => x.CurrentWeight).When(x => x.Goal == Domain.User.GoalType.LooseWeight).WithMessage("Target weight should be less than current weight");
            RuleFor(x => x.DateOfBirth).LessThan(DateTimeOffset.Now).WithMessage("Please, enter valid date of birth");
        }
    }
}
