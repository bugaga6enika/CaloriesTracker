using CaloriesTracker.Domain.InternalAuth;
using CaloriesTracker.Infrastructure.Rest;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.InternalAuth
{
    public class GetCurrentAuthTokenQueryHandler : IRequestHandler<GetCurrentAuthTokenQuery, AuthToken>
    {
        private readonly IAuthTokenProvider _authTokenProvider;

        public GetCurrentAuthTokenQueryHandler(IAuthTokenProvider authTokenProvider)
        {
            _authTokenProvider = authTokenProvider;
        }

        public Task<AuthToken> Handle(GetCurrentAuthTokenQuery request, CancellationToken cancellationToken)
            => _authTokenProvider.GetTokenAsync();
    }
}