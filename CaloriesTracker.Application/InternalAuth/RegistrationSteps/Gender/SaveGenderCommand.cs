using MediatR;

namespace CaloriesTracker.Application.InternalAuth.RegistrationSteps.Gender
{
    public class SaveGenderCommand : IRequest<bool>
    {
        public SaveGenderCommand(Domain.InternalAuth.Gender gender)
        {
            Gender = gender;
        }

        public Domain.InternalAuth.Gender Gender { get; }
    }
}
