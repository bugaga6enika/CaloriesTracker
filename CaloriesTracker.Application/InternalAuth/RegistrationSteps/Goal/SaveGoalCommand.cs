using CaloriesTracker.Domain.InternalAuth;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Goal
{
    public class SaveGoalCommand : IRequest<bool>
    {
        public SaveGoalCommand(GoalType goal)
        {
            Goal = goal;
        }

        public GoalType Goal { get; }
    }
}