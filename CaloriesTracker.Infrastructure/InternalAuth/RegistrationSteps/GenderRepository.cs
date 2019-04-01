using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using CaloriesTracker.Infrastructure.LocalStorage;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.InternalAuth.RegistrationSteps
{
    internal class GenderRepository : LocalStorageRepository<RegistrationInfo>, IGenderRepository
    {
        public GenderRepository() : base(LocalStorageKeys.RegistrationInfo)
        {
        }

        public async Task<Gender> GetCurrent()
        {
            var registrationInfo = await Get();
            return registrationInfo.Gender;
        }

        public async Task<bool> Save(Gender gender, CancellationToken cancellationToken)
        {
            var registrationInfo = await Get();

            registrationInfo.Gender = gender;

            await Save(registrationInfo);
            await ForseWrite();

            return true;
        }
    }
}