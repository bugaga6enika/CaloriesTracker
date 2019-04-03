using FluentValidation;
using CaloriesTracker.Domain.Validation.Rules;

namespace CaloriesTracker.Application.User
{
    internal sealed class SendCredentialsCommandValidator : AbstractValidator<SendCredentialsCommand>
    {
        public SendCredentialsCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Must(x => x.DoesGreaterOrEqualThen(10)).WithMessage("Password must be greater then 10")
                .Must(x => x.DoesContainDigit()).WithMessage("Password must contain at least one digit")
                .Must(x => x.DoesContainNonAlphanumeric()).WithMessage("Password must contain a non alphanumeric character");
        }
    }
}
