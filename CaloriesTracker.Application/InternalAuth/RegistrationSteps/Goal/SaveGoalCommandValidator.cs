using FluentValidation;

namespace CaloriesTracker.Application.User.RegistrationSteps.Goal
{
    public class SaveGoalCommandValidator : AbstractValidator<SaveGoalCommand>
    {
        public SaveGoalCommandValidator()
        {
            RuleFor(x => x.Goal).Must(goal => goal != Domain.User.GoalType.NotSpecified).WithMessage("Please, specify your goal");
        }
    }
}
