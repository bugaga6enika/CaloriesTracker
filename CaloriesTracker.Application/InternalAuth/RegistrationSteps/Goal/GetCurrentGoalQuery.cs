using CaloriesTracker.Domain.InternalAuth;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Goal
{
    public class GetCurrentGoalQuery : IRequest<GoalType>
    {
    }
}
