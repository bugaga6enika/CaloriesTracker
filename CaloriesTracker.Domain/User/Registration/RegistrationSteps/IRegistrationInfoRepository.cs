using System.Threading.Tasks;

namespace CaloriesTracker.Domain.User.RegistrationSteps
{
    public interface IRegistrationInfoRepository
    {
        Task<RegistrationInfo> GetCurrent();
    }
}