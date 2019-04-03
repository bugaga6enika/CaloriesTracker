using CaloriesTracker.Domain.User.RegistrationSteps;
using MediatR;

namespace CaloriesTracker.Application.User.RegistrationSteps.Credentials
{
    public class GetRegistrationInfoQuery : IRequest<RegistrationInfo>
    {
    }
}