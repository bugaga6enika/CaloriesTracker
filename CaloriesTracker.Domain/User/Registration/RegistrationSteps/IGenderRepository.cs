using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Domain.User.RegistrationSteps
{
    public interface IGenderRepository
    {
        Task<bool> Save(GenderType gender, CancellationToken cancellationToken);
        Task<GenderType> GetCurrent();
    }
}
