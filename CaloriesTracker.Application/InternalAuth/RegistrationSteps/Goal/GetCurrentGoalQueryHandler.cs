using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Goal
{
    public class GetCurrentGoalQueryHandler : IRequestHandler<GetCurrentGoalQuery, GoalType>
    {
        private readonly IGoalRepository _goalRepository;

        public GetCurrentGoalQueryHandler(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public Task<GoalType> Handle(GetCurrentGoalQuery request, CancellationToken cancellationToken)
            => _goalRepository.GetCurrent();
    }
}
