using CaloriesTracker.Domain.User;
using MediatR;

namespace CaloriesTracker.Application.User
{
    public class GetCurrentAuthTokenQuery : IRequest<AuthToken> { }
}