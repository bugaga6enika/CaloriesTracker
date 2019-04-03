using CaloriesTracker.Domain.User.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User.RegistrationSteps.Goal
{
    public class SaveGoalCommandHandler : IRequestHandler<SaveGoalCommand, bool>
    {
        private readonly IGoalRepository _goalRepository;

        public SaveGoalCommandHandler(IGoalRepository goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public Task<bool> Handle(SaveGoalCommand request, CancellationToken cancellationToken)
            => _goalRepository.Save(request.Goal, cancellationToken);
    }
}
