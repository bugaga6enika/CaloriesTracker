using FluentValidation;
using CaloriesTracker.Domain.Validation.Rules;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Credentials
{
    internal sealed class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
    {
        public RegistrationCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).Must(x => x.IsEmailValid()).WithMessage("Email is not valid");
            RuleFor(x => x.Goal).Must(x => x.IsValid()).WithMessage("Goal is not specified");
        }
    }
}
