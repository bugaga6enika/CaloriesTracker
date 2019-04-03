using CaloriesTracker.Domain.User;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Rest
{
    public interface IAuthTokenProvider
    {
        Task<AuthToken> GetTokenAsync();
    }
}
