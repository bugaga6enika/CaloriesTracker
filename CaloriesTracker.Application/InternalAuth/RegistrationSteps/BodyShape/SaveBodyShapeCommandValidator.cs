using FluentValidation;

namespace CaloriesTracker.Application.User.RegistrationSteps.BodyShape
{
    public class SaveBodyShapeCommandValidator : AbstractValidator<SaveBodyShapeCommand>
    {
        public SaveBodyShapeCommandValidator()
        {
            RuleFor(x => x.CurrentWeight).GreaterThan(0).WithMessage("Current weight must be greater than 0")
                .GreaterThan(x => x.TargetWeight).When(x => x.Goal == Domain.User.GoalType.LooseWeight).WithMessage("Current weight should be greater than target weight")
                .LessThan(x => x.TargetWeight).When(x => x.Goal == Domain.User.GoalType.GainWeight).WithMessage("Current weight should be less than target weight");
            RuleFor(x => x.TargetWeight).GreaterThan(0).When(x => x.Goal != Domain.User.GoalType.SaveWeight).WithMessage("Target weight must be greater than 0")
                .GreaterThan(x => x.CurrentWeight).When(x => x.Goal == Domain.User.GoalType.GainWeight).WithMessage("Target weight should be greater than current weight")
                .LessThan(x => x.CurrentWeight).When(x => x.Goal == Domain.User.GoalType.LooseWeight).WithMessage("Target weight should be less than current weight");
            RuleFor(x => x.Height).GreaterThan(0).WithMessage("Height must be greater than 0");
        }
    }
}