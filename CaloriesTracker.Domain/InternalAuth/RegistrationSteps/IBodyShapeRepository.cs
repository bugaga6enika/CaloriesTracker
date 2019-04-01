using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Domain.InternalAuth.RegistrationSteps
{
    public interface IBodyShapeRepository
    {
        Task<bool> Save(BodyShape bodyShape, CancellationToken cancellationToken);
        Task<BodyShape> GetCurrent();
    }
}