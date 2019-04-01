using FluentValidation;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Gender
{
    public class SaveGenderCommandValidator : AbstractValidator<SaveGenderCommand>
    {
        public SaveGenderCommandValidator()
        {
            RuleFor(x => x.Gender).Must(gender => gender != Domain.InternalAuth.Gender.NotSpecified).WithMessage("Please, specify gender");
        }
    }
}