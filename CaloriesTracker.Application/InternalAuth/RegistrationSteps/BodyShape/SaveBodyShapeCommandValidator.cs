using FluentValidation;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.BodyShape
{
    public class SaveBodyShapeCommandValidator : AbstractValidator<SaveBodyShapeCommand>
    {
        public SaveBodyShapeCommandValidator()
        {
            RuleFor(x => x.CurrentWeight).GreaterThan(0).WithMessage("Current weight must be greater than 0");
            RuleFor(x => x.TargetWeight).GreaterThan(0).When(x => x.Goal != Domain.InternalAuth.GoalType.SaveWeight).WithMessage("Target weight must be greater than 0");
            RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than 0");
        }
    }
}