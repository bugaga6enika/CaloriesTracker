using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.BodyShape
{
    public class SaveBodyShapeCommandHandler : IRequestHandler<SaveBodyShapeCommand, bool>
    {
        private readonly IBodyShapeRepository _bodyShapeRepository;

        public SaveBodyShapeCommandHandler(IBodyShapeRepository bodyShapeRepository)
        {
            _bodyShapeRepository = bodyShapeRepository;
        }

        public Task<bool> Handle(SaveBodyShapeCommand request, CancellationToken cancellationToken)
            => _bodyShapeRepository.Save(Configuration.Mappings.Mapper.Current.Map<Domain.InternalAuth.BodyShape>(request), cancellationToken);
    }
}
