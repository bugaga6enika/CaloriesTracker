using MediatR;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Gender
{
    public class GetCurrentGenderQuery : IRequest<Domain.InternalAuth.Gender>
    {
    }
}
