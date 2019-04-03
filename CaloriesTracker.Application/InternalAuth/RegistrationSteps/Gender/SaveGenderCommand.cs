using MediatR;

namespace CaloriesTracker.Application.User.RegistrationSteps.Gender
{
    public class SaveGenderCommand : IRequest<bool>
    {
        public SaveGenderCommand(Domain.User.GenderType gender)
        {
            Gender = gender;
        }

        public Domain.User.GenderType Gender { get; }
    }
}
