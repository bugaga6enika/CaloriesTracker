using FluentValidation;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Goal
{
    public class SaveGoalCommandValidator : AbstractValidator<SaveGoalCommand>
    {
        public SaveGoalCommandValidator()
        {
            RuleFor(x => x.Goal).Must(goal => goal != Domain.InternalAuth.GoalType.NotSpecified).WithMessage("Please, specify your goal");
        }
    }
}
