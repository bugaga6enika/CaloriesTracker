using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Rest
{
    public class AuthMiddleware : IRequestProcessor
    {
        private readonly IAuthTokenProvider _authTokenProvider;

        public AuthMiddleware(IAuthTokenProvider authTokenProvider)
        {
            _authTokenProvider = authTokenProvider;
        }

        public async Task<HttpRequestMessage> ProcessRequestAsync(HttpRequestMessage request)
        {
            var auth = request.Headers.Authorization;

            if (auth != null)
            {
                var currentToken = await _authTokenProvider.GetTokenAsync();
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, currentToken.AccessToken);
            }

            return request;
        }
    }
}
