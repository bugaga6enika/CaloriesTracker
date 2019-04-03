using CaloriesTracker.Domain.User.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.User.RegistrationSteps.BodyShape
{
    public class GetCurrentBodyShapeQueryHandler : IRequestHandler<GetCurrentBodyShapeQuery, Domain.User.BodyShape>
    {
        private readonly IBodyShapeRepository _bodyShapeRepository;

        public GetCurrentBodyShapeQueryHandler(IBodyShapeRepository bodyShapeRepository)
        {
            _bodyShapeRepository = bodyShapeRepository;
        }

        public Task<Domain.User.BodyShape> Handle(GetCurrentBodyShapeQuery request, CancellationToken cancellationToken)
            => _bodyShapeRepository.GetCurrent();
    }
}
