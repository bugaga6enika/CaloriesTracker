using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.BodyShape
{
    public class GetCurrentBodyShapeQueryHandler : IRequestHandler<GetCurrentBodyShapeQuery, Domain.InternalAuth.BodyShape>
    {
        private readonly IBodyShapeRepository _bodyShapeRepository;

        public GetCurrentBodyShapeQueryHandler(IBodyShapeRepository bodyShapeRepository)
        {
            _bodyShapeRepository = bodyShapeRepository;
        }

        public Task<Domain.InternalAuth.BodyShape> Handle(GetCurrentBodyShapeQuery request, CancellationToken cancellationToken)
            => _bodyShapeRepository.GetCurrent();
    }
}
