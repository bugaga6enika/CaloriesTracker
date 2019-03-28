using CaloriesTracker.Domain.InternalAuth;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth
{
    public class GetCurrentAuthTokenQuery : IRequest<AuthToken> { }
}