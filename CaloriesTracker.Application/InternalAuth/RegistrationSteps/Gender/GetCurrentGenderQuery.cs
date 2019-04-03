using MediatR;

namespace CaloriesTracker.Application.User.RegistrationSteps.Gender
{
    public class GetCurrentGenderQuery : IRequest<Domain.User.GenderType>
    {
    }
}
