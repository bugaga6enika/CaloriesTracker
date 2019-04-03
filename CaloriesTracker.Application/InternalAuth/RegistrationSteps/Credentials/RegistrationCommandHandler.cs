using CaloriesTracker.Domain.Abstractions.Core;
using CaloriesTracker.Domain.User;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User.RegistrationSteps.Credentials
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, OperationResult>
    {
        private readonly IInternalAuthRepository _internalAuthRepository;

        public RegistrationCommandHandler(IInternalAuthRepository internalAuthRepository)
        {
            _internalAuthRepository = internalAuthRepository;
        }

        public Task<OperationResult> Handle(RegistrationCommand request, CancellationToken cancellationToken)
            => _internalAuthRepository.Register(Configuration.Mappings.Mapper.Current.Map<RegistrationRequest>(request), cancellationToken);
    }
}
