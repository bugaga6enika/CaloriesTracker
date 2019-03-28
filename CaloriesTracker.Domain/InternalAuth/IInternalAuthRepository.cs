using CaloriesTracker.Domain.Abstractions.Core;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Domain.InternalAuth
{
    public interface IInternalAuthRepository
    {
        Task<OperationResult> Register(RegistrationRequest registrationRequest, CancellationToken cancellationToken);
        Task<AuthToken> GetAuthToken(Credentials credentials, CancellationToken cancellationToken);        
    }
}