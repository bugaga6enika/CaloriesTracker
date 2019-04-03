using CaloriesTracker.Domain.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User
{
    public class SendCredentialsCommandHandler : IRequestHandler<SendCredentialsCommand, AuthToken>
    {
        private readonly IInternalAuthRepository _internalAuthRepository;

        public SendCredentialsCommandHandler(IInternalAuthRepository internalAuthRepository)
        {
            _internalAuthRepository = internalAuthRepository;
        }

        public Task<AuthToken> Handle(SendCredentialsCommand request, CancellationToken cancellationToken)
            => _internalAuthRepository.GetAuthToken(Configuration.Mappings.Mapper.Current.Map<Credentials>(request), cancellationToken);
    }
}