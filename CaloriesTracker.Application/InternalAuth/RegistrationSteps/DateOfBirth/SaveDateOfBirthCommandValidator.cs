using FluentValidation;
using System;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.DateOfBirth
{
    public class SaveDateOfBirthCommandValidator : AbstractValidator<SaveDateOfBirthCommand>
    {
        public SaveDateOfBirthCommandValidator()
        {
            RuleFor(x => x.DateOfBirth).LessThan(DateTimeOffset.Now).WithMessage("Please, enter valid date of birth");
        }
    }
}