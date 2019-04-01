using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Credentials
{
    public class GetRegistrationInfoQueryHandler : IRequestHandler<GetRegistrationInfoQuery, RegistrationInfo>
    {
        private readonly IRegistrationInfoRepository _registrationInfoRepository;

        public GetRegistrationInfoQueryHandler(IRegistrationInfoRepository registrationInfoRepository)
        {
            _registrationInfoRepository = registrationInfoRepository;
        }

        public Task<RegistrationInfo> Handle(GetRegistrationInfoQuery request, CancellationToken cancellationToken)
            => _registrationInfoRepository.GetCurrent();
    }
}
