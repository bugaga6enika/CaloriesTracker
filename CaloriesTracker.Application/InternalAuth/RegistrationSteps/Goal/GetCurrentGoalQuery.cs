using CaloriesTracker.Domain.User;
using MediatR;

namespace CaloriesTracker.Application.User.RegistrationSteps.Goal
{
    public class GetCurrentGoalQuery : IRequest<GoalType>
    {
    }
}
