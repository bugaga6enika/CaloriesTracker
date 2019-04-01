using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Domain.InternalAuth.RegistrationSteps
{
    public interface IGenderRepository
    {
        Task<bool> Save(Gender gender, CancellationToken cancellationToken);
        Task<Gender> GetCurrent();
    }
}
