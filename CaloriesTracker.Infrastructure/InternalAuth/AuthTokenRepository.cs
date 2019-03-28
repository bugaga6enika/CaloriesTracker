using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Infrastructure.Configuration;
using CaloriesTracker.Infrastructure.Rest.Repositories;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.InternalAuth
{
    public class AuthTokenRepository : RestRepositoryBase<IInternalAuthRestRepository>, IInternalAuthRepository
    {
        public AuthTokenRepository(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<AuthToken> GetAuthToken(Credentials credentials, CancellationToken cancellationToken)
        {
            try
            {
                var authTokenDto = await RestService.Get(credentials, cancellationToken);
                var authToken = AuthToken.Create(authTokenDto.AccessToken, authTokenDto.RefreshToken, authTokenDto.ExpiresIn);

                if (authToken.IsValid)
                {
                    Settings.CurrentToken = authTokenDto;
                }

                return authToken;
            }
            catch (System.Exception e)
            {
                ForwardException(e);

                return AuthToken.Empty;
            }
        }       

        public async Task<OperationResult> Register(RegistrationRequest registrationRequest, CancellationToken cancellationToken)
        {
            try
            {
                var response = await RestService.Register(registrationRequest.Email, cancellationToken);

                return response.Success ? OperationResult.SuccessOperation : OperationResult.FailedOperation(new System.Exception(response.Content));
            }
            catch (System.Exception e)
            {
                ForwardException(e);

                return OperationResult.FailedOperation(e);
            }
        }
    }
}
