using CaloriesTracker.Domain.User.RegistrationSteps;
using CaloriesTracker.Infrastructure.LocalStorage;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.User.RegistrationSteps
{
    internal class RegistrationInfoRepository : LocalStorageRepository<RegistrationInfo>, IRegistrationInfoRepository
    {
        public RegistrationInfoRepository() : base(LocalStorageKeys.RegistrationInfo)
        {
        }

        public async Task<RegistrationInfo> GetCurrent()
        {
            return await Get();
        }
    }
}
