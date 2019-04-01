using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using CaloriesTracker.Infrastructure.LocalStorage;
using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.InternalAuth.RegistrationSteps
{
    internal class DateOfBirthRepository : LocalStorageRepository<RegistrationInfo>, IDateOfBirthRepository
    {
        public DateOfBirthRepository() : base(LocalStorageKeys.RegistrationInfo)
        {

        }

        public async Task<DateTimeOffset> GetCurrent()
        {
            var registrationInfo = await Get();

            return registrationInfo.DateOfBirth;
        }

        public async Task<bool> Save(DateTimeOffset dateOfBirth, CancellationToken cancellationToken)
        {
            var registrationInfo = await Get();

            registrationInfo.DateOfBirth = dateOfBirth;

            await Save(registrationInfo);
            await ForseWrite();

            return true;
        }
    }
}
