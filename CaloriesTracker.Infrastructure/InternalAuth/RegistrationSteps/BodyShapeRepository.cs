using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using CaloriesTracker.Infrastructure.LocalStorage;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.InternalAuth.RegistrationSteps
{
    internal class BodyShapeRepository : LocalStorageRepository<RegistrationInfo>, IBodyShapeRepository
    {
        public BodyShapeRepository() : base(LocalStorageKeys.RegistrationInfo)
        {
        }

        public async Task<BodyShape> GetCurrent()
        {
            var registrationInfo = await Get();
            return new BodyShape
            {
                CurrentWeight = registrationInfo.CurrentWeight,
                TargetWeight = registrationInfo.TargetWeight,
                Height = registrationInfo.Height
            };
        }

        public async Task<bool> Save(BodyShape bodyShape, CancellationToken cancellationToken)
        {
            var registrationInfo = await Get();

            registrationInfo.CurrentWeight = bodyShape.CurrentWeight;
            registrationInfo.TargetWeight = bodyShape.TargetWeight;
            registrationInfo.Height = bodyShape.Height;

            await Save(registrationInfo);
            await ForseWrite();

            return true;
        }
    }
}