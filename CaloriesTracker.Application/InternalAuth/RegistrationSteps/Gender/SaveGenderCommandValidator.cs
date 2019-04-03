using FluentValidation;

namespace CaloriesTracker.Application.User.RegistrationSteps.Gender
{
    public class SaveGenderCommandValidator : AbstractValidator<SaveGenderCommand>
    {
        public SaveGenderCommandValidator()
        {
            RuleFor(x => x.Gender).Must(gender => gender != Domain.User.GenderType.NotSpecified).WithMessage("Please, specify gender");
        }
    }
}