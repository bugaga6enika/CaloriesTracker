using CaloriesTracker.Domain.User;
using CaloriesTracker.Domain.User.RegistrationSteps;
using CaloriesTracker.Infrastructure.LocalStorage;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.User.RegistrationSteps
{
    internal class GenderRepository : LocalStorageRepository<RegistrationInfo>, IGenderRepository
    {
        public GenderRepository() : base(LocalStorageKeys.RegistrationInfo)
        {
        }

        public async Task<GenderType> GetCurrent()
        {
            var registrationInfo = await Get();
            return registrationInfo.Gender;
        }

        public async Task<bool> Save(GenderType gender, CancellationToken cancellationToken)
        {
            var registrationInfo = await Get();

            registrationInfo.Gender = gender;

            await Save(registrationInfo);
            await ForseWrite();

            return true;
        }
    }
}