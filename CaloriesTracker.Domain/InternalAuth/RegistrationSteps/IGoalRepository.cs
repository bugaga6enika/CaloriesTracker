using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Domain.InternalAuth.RegistrationSteps
{
    public interface IGoalRepository
    {
        Task<bool> Save(GoalType goal, CancellationToken cancellationToken);
        Task<GoalType> GetCurrent();
    }
}
