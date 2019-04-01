using CaloriesTracker.Domain.InternalAuth;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.BodyShape
{
    public class SaveBodyShapeCommand : IRequest<bool>
    {
        public SaveBodyShapeCommand(double currentWeight, double? targetWeight, int height, GoalType goal)
        {
            CurrentWeight = currentWeight;
            TargetWeight = targetWeight;
            Height = height;
            Goal = goal;
        }

        public double CurrentWeight { get; }
        public double? TargetWeight { get; }
        public int Height { get; }
        public GoalType Goal { get; }
    }
}
