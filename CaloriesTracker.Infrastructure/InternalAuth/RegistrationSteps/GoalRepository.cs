using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using CaloriesTracker.Infrastructure.LocalStorage;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.InternalAuth.RegistrationSteps
{
    internal class GoalRepository : LocalStorageRepository<RegistrationInfo>, IGoalRepository
    {
        public GoalRepository() : base(LocalStorageKeys.RegistrationInfo)
        {
        }

        public async Task<GoalType> GetCurrent()
        {
            var registrationInfo = await Get();
            return registrationInfo.Goal;
        }

        public async Task<bool> Save(GoalType goal, CancellationToken cancellationToken)
        {
            var registrationInfo = await Get();

            registrationInfo.Goal = goal;

            await Save(registrationInfo);
            await ForseWrite();

            return true;
        }
    }
}
