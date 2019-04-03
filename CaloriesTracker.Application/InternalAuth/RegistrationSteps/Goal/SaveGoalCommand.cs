using CaloriesTracker.Domain.User;
using MediatR;

namespace CaloriesTracker.Application.User.RegistrationSteps.Goal
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