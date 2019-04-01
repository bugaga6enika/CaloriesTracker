using CaloriesTracker.Domain.InternalAuth.RegistrationSteps;
using MediatR;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Credentials
{
    public class GetRegistrationInfoQuery : IRequest<RegistrationInfo>
    {
    }
}