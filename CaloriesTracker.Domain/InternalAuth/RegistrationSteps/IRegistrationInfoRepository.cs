using System.Threading.Tasks;

namespace CaloriesTracker.Domain.InternalAuth.RegistrationSteps
{
    public interface IRegistrationInfoRepository
    {
        Task<RegistrationInfo> GetCurrent();
    }
}