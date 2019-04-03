using CaloriesTracker.Domain.User.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User.RegistrationSteps.Gender
{
    public class GetCurrentGenderQueryHandler : IRequestHandler<GetCurrentGenderQuery, Domain.User.GenderType>
    {
        private readonly IGenderRepository _genderRepository;

        public GetCurrentGenderQueryHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public Task<Domain.User.GenderType> Handle(GetCurrentGenderQuery request, CancellationToken cancellationToken)
            => _genderRepository.GetCurrent();
    }
}
