using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Infrastructure.InternalAuth;
using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Rest.Repositories
{
    public interface IInternalAuthRestRepository : IRestRepository
    {
        [Get("/account/token")]
        Task<AuthTokenDto> Get(Credentials credentials, CancellationToken cancelationToken);

        [Post("/account/token")]
        Task<AuthTokenDto> Update(string refreshToken, CancellationToken cancelationToken);

        [Get("/account/registration")]
        Task<JsonResponse<string>> Register(string email, CancellationToken cancelationToken);
    }
}
