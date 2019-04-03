using CaloriesTracker.Domain.User;
using MediatR;

namespace CaloriesTracker.Application.User.RegistrationSteps.BodyShape
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
