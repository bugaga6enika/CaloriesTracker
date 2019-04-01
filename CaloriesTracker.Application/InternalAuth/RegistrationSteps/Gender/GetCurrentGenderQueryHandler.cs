using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Gender
{
    public class GetCurrentGenderQueryHandler : IRequestHandler<GetCurrentGenderQuery, Domain.InternalAuth.Gender>
    {
        private readonly IGenderRepository _genderRepository;

        public GetCurrentGenderQueryHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public Task<Domain.InternalAuth.Gender> Handle(GetCurrentGenderQuery request, CancellationToken cancellationToken)
            => _genderRepository.GetCurrent();
    }
}
